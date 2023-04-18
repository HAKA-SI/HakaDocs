using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]
    public class OrdersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public OrdersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                        order.AmountPaid =Convert.ToDecimal(model.Details.AmountPaid);
                    }
                    else
                    {
                        order.FullyPaid=false;
                        order.Paid=false;
                        order.AmountPaid = 0;
                    }


                    context.Add(order);
                    await context.SaveChangesAsync();

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
                        context.Add(orderLine);
                        await context.SaveChangesAsync();
                        if (item.WithSerialNumber)
                        {
                            foreach (var subProd in item.SubProductSNs)
                            {
                                context.OrderLineSubProductSNs.Add(
                                    new OrderLineSubProductSN { SubProductSNId = subProd.Id, OrderLineId = orderLine.Id, DiscountAmout = subProd.Discount }
                                );
                            }
                            // storeid doit etre egal a null
                            await context.SaveChangesAsync();
                            // orderlineSubProductsSns
                        }
                    }
                    done = true;
                    transaction.Commit();
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