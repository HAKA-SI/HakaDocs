using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]
    public class OrdersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly StockAlertService _stockAlertService;



        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config, StockAlertService stockAlertService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
            _stockAlertService = stockAlertService;

        }

        [HttpPost("SaveClientOrder/{hakaDocClientId}")]
        public async Task<ActionResult> SaveClientOrder(int hakaDocClientId, PhysicalBasketDto model)
        {
            var loggeduserId = User.GetUserId();
            if (!(await _unitOfWork.AuthRepository.CanDoAction(loggeduserId, hakaDocClientId))) return Unauthorized();

            var done = false;
            var context = _unitOfWork.GetDataContext();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    //1. enregistrement dans order
                    //2. enregistrement de stockMvt
                    //3. enregistrement de orderline
                    //4. enregistrement de inventOp




                    //enregistrement de l'order
                    var order = new Order
                    {
                        OrderTypeId = 1,
                        OrderLabel = model.Details.OrderNum,
                        // OrderNum = model.Details.OrderNum,
                        OrderDate = model.Details.OrderDate,
                        CustomerId = model.CustomerId,
                        Validated = true,
                        Expired = true,
                        Cancelled = false,
                        Delivered = model.Details.Delivered,
                        Completed = true,
                        InsertUserId = loggeduserId,
                        UpdateUserId = loggeduserId,
                        // Paid = (model.Details.PaimentType == 1 || (model.Details.PaimentType == 2 && model.Details.AmountPaid > 0)) ? true : false,
                        TotalHT = model.TotalHT,
                        DiscountAmount = (model.Total - model.SubTotal),
                        AmountTTC = model.Total,
                        Overdue = (model.Details.PaimentType == 2 && model.Details.AmountPaid > model.Total) ? true : false
                    };

                    if (model.Details.PaimentType == 1)// payé entierement
                    {
                        order.AmountPaid = order.AmountTTC;
                        order.Paid = true;
                        order.FullyPaid = true;
                    }
                    else if (model.Details.PaimentType == 2)
                    {
                        order.Paid = true;
                        order.FullyPaid = false;
                        order.AmountPaid = Convert.ToDecimal(model.Details.AmountPaid);
                    }
                    else
                    {
                        order.FullyPaid = false;
                        order.Paid = false;
                        order.AmountPaid = 0;
                    }
                    context.Add(order);

                    // enregistrement de StockMvt
                    int stockEntryTypeId = _config.GetValue<int>("AppSettings:inventOpType:PhysicalInventOpType");
                    var stockmvt = new StockMvt
                    {
                        InventOpTypeId = stockEntryTypeId,
                        CustomerId = model.CustomerId,
                        RefNum = model.Details.OrderNum,
                        Note = model.Details.Observation,
                        MvtDate = model.Details.OrderDate,
                        InsertUserId = loggeduserId
                    };

                    context.StockMvts.Add(stockmvt);
                    await context.SaveChangesAsync();

                    var productList = await context.SubProducts.Where(a => model.Products.Select(p => p.Id).Contains(a.Id)).ToListAsync();
                    foreach (var item in model.Products)
                    {
                        var orderLine = new OrderLine
                        {
                            OrderId = order.Id,
                            OrderLineLabel = model.Details.OrderNum,
                            SubProductId = item.Id,
                            Qty = item.Newqty,
                            UnitPrice = item.UnitPrice,

                            TotalHT = 0,
                            Paid = (model.Details.PaimentType == 1 || (model.Details.PaimentType == 2 && model.Details.AmountPaid > 0)) ? true : false,
                            // public decimal TotalHT { get; set; }
                            // public decimal DiscountAmount { get; set; }
                            // public decimal AmountHT { get; set; }
                            // public decimal TVA { get; set; }
                            // public decimal TVAAmount { get; set; }
                            // public decimal AmountTTC { get; set; }
                            InsertUserId = loggeduserId,
                            UpdateUserId = loggeduserId

                        };
                        context.OrderLines.Add(orderLine);

                        var currentProd = productList.FirstOrDefault(a => a.Id == item.Id);
                        var currentStore = await context.StoreProducts.FirstOrDefaultAsync(a => a.SubProductId == item.Id);

                        // enregistrement inventOp
                        InventOp inv = new InventOp
                        {
                            InsertUserId = stockmvt.InsertUserId,
                            InventOpTypeId = stockmvt.InventOpTypeId,
                            OpDate = stockmvt.MvtDate,
                            FromStoreId = model.StoreId,
                            CustomerId = model.CustomerId,
                            FormNum = stockmvt.RefNum,
                            SubProductId = item.Id,
                            Quantity = item.Newqty,
                            OrderId = order.Id
                        };
                        context.InventOps.Add(inv);
                        await context.SaveChangesAsync();

                        // enregistrement InventOpStockMvts
                        context.StockMvtInventOps.Add(new StockMvtInventOp { InventOpId = inv.Id, StockMvtId = stockmvt.Id });


                        //mise a jour de la quantité dans le store product seulement lorsque le produit n'a pas de sn
                        if (currentStore != null)
                        {
                            currentStore.Quantity -= item.Newqty;
                            context.StoreProducts.Update(currentStore);
                        }

                        //mise a jour de la quantitê dans SubProduct
                        var subproduct = await _unitOfWork.ProductRepository.GetSubProduct(item.Id);
                        subproduct.Quantity -= item.Newqty;
                        context.Update(subproduct);
                        await context.SaveChangesAsync();

                        //enregistrement StockHistory
                        int in_stockEntryActionId = _config.GetValue<int>("AppSettings:outStockEntryHistoryId");
                        StockHistory h = new StockHistory
                        {
                            OpDate = stockmvt.MvtDate,
                            UserId = loggeduserId,
                            InventOpId = inv.Id,
                            StockHistoryActionId = in_stockEntryActionId,
                            StoreId = model.StoreId,
                            SubProductId = item.Id
                        };
                        StockHistory history = await _unitOfWork.ProductRepository.StoreSubProductHistory(model.StoreId, item.Id);
                        h.OldQty = history.NewQty;
                        h.NewQty = (history.NewQty - item.Newqty);
                        h.Delta = (h.NewQty - h.OldQty);

                        context.Add(h);
                        await context.SaveChangesAsync();

                        if (item.WithSerialNumber)
                        {
                            foreach (var subProd in item.SubProductSNs)
                            {
                                //enregistrement orderLine
                                context.OrderLineSubProductSNs.Add(new OrderLineSubProductSN { SubProductSNId = subProd.Id, OrderLineId = orderLine.Id, DiscountAmout = subProd.Discount });
                                // orderlineSubProductsSns
                                context.InventOpSubProductSNs.Add(new InventOpSubProductSN { InventOpId = inv.Id, SubProductSNId = subProd.Id });
                                //updating SubProductSN
                                var subPorductSN = await context.SubProductSNs.FirstOrDefaultAsync(a => a.Id == subProd.Id);
                                subPorductSN.StoreId = null;
                                context.Update(subPorductSN);
                                //delete storeProductLine
                                var storeProductFromDb = await context.StoreProducts.FirstOrDefaultAsync(a => a.SubProductSNId == subProd.Id);
                                if (storeProductFromDb != null)
                                {
                                    storeProductFromDb.Active = false;
                                }
                                await context.SaveChangesAsync();
                            }
                        }
                    }
                    done = true;
                    transaction.Commit();
                   // Task.Run(async () => await _stockAlertService.SendStockNotification(model.Products.Select(a => a.Id).ToList(), hakaDocClientId));
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
            if (done) return Ok();
            return BadRequest();
        }

    }
}