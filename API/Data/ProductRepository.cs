using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Dtos;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.SignalR;
using API.SignalR;

namespace API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IHubContext<StockAlertHub> _stockHubContext;
        public ProductRepository(DataContext context, IConfiguration config, IHubContext<StockAlertHub> stockHubContext)
        {
            _context = context;
            _config = config;
            _stockHubContext = stockHubContext;
        }


        public async Task<List<Category>> GetCategories(int haKaDocClientId, int productGroupId)
        {
            return await _context.Categories.AsNoTracking().Where(a => a.HaKaDocClientId == haKaDocClientId && a.ProductGroupId == productGroupId).OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesWithProducts(int haKaDocClientId, int productGroupId)
        {
            return await _context.Categories.Include(a => a.HaKaDocClient).Include(a => a.Products).Where(a => a.HaKaDocClientId == haKaDocClientId && a.ProductGroupId == productGroupId).OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(a => a.Id == categoryId);
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(a => a.Id == productId);
        }

        public async Task<Product> GetProductWithDetails(int productId)
        {
            return await _context.Products.Include(c => c.Category).FirstOrDefaultAsync(a => a.Id == productId);

        }

        public async Task<List<Product>> GetProducts(int hakaDocClientId)
        {
            return await _context.Products.Where(a => a.HaKaDocClientId == hakaDocClientId).ToListAsync();

        }

        public async Task<List<Product>> GetProductsWithDetails(int hakaDocClientId, int productGroupId)
        {
            return await _context.Products.Include(a => a.Category).Where(a => a.HaKaDocClientId == hakaDocClientId && a.Category.ProductGroupId == productGroupId).ToListAsync();
        }

        public async Task<List<SubProduct>> GetSubProducts(int haKaDocClientId, int productGroupId)
        {
            return await _context.SubProducts.Include(a => a.Photos)
                                             .Include(a => a.Product)
                                             .ThenInclude(a => a.Category)
                                             .Where(a => a.HaKaDocClientId == haKaDocClientId && a.Product.Category.ProductGroupId == productGroupId)
                                             .ToListAsync();
        }

        public async Task<SubProduct> GetSubProduct(int subProductId)
        {
            return await _context.SubProducts.Include(a => a.Photos)
                                             .Include(a => a.Product)
                                             .ThenInclude(a => a.Category)
                                             .FirstOrDefaultAsync(a => a.Id == subProductId);
        }

        public async Task<List<SubProduct>> GetSubProductWithSNs(int hakaDocClientId, int productGroupId)
        {
            return await _context.SubProducts.Include(a => a.Photos)
                                            .Include(a => a.Product)
                                            .ThenInclude(a => a.Category)
                                            .Where(a => a.HaKaDocClientId == hakaDocClientId && a.WithSerialNumber == true && a.Product.Category.ProductGroupId == productGroupId)
                                            .ToListAsync();
        }

        public async Task<List<SubProduct>> GetSubProductWithoutSNs(int hakaDocClientId, int productGroupId)
        {
            return await _context.SubProducts.Include(a => a.Photos)
                                            .Include(a => a.Product)
                                            .ThenInclude(a => a.Category)
                                            .Where(a => a.HaKaDocClientId == hakaDocClientId && a.WithSerialNumber == false && a.Product.Category.ProductGroupId == productGroupId)
                                            .ToListAsync();
        }

        public async Task<StockHistory> StoreSubProductHistory(int storeId, int subProductId)
        {
            return await _context.StockHistories.Where(a => a.StoreId == storeId && a.SubProductId == subProductId).OrderByDescending(a => a.Id).FirstOrDefaultAsync();
        }

        public async Task<StoreProduct> StoreProduct(int storeId, int subProductId)
        {
            return await _context.StoreProducts.FirstOrDefaultAsync(a => a.StoreId == storeId && a.SubProductId == subProductId);
        }

        public async Task<List<SubProductSN>> GetSubProductSnBySubProductId(int subProductId)
        {
            return await _context.SubProductSNs.Include(a => a.SubProduct)
                                                .ThenInclude(a => a.Photos)
                                                .Include(a => a.SubProduct.Product)
                                                .Include(a => a.SubProduct.Product.Category)
                                                .Where(a => a.SubProductId == subProductId).ToListAsync();
        }

        public async Task<List<SubProductSN>> getInventOpSubProductSNs(int inventOpId, int subProductId)
        {
            var invProds = await _context.InventOpSubProductSNs.Where(a => a.InventOpId == inventOpId)
                                                                .ToListAsync();
            var ids = invProds.Select(a => a.SubProductSNId);

            return await _context.SubProductSNs.Include(a => a.SubProduct)
                                                .ThenInclude(a => a.Photos)
                                                .Include(a => a.SubProduct.Product)
                                                .Include(a => a.SubProduct.Product.Category)
                                                .Where(a => ids.Contains(a.Id)).ToListAsync();

        }

        public async Task<SubProductSN> GetSubProductSN(int subproductId)
        {
            return await _context.SubProductSNs.FirstOrDefaultAsync(a => a.Id == subproductId);
        }

        public async Task<InventOp> GetInventOpById(int inventOpId)
        {
            return await _context.InventOps.Include(a => a.StockMvtInventOps)
                                             .Include(a => a.InventOpSubProductSNs)
                                             .Include(a => a.StockHistory)
                                             .FirstOrDefaultAsync(a => a.Id == inventOpId);
        }

        public async Task SendStockNotification(List<int> subproductIds, int hakaDocClientId)
        {

            /* This code is responsible for saving and sending a stock alert notification to all users of a specific
            client when the quantity of a subproduct reaches its reorder level. */


            int notifificationTypeId = _config.GetValue<int>("AppSettings:notificationType:stockAlertTypeId");

            var clientUsers = await _context.Users.Where(a => a.HaKaDocClientId == hakaDocClientId).ToListAsync();
            foreach (var item in subproductIds)
            {
                var subProduct = await _context.SubProducts.Include(a => a.Product).FirstOrDefaultAsync(a => a.Id == item);
                if (subProduct.Quantity <= subProduct.ReorderLevel)//critical stock reched
                {
                    var content = "<b>Alerte stock:</b> Vous avez atteint votre stock critique pour <b>" + subProduct.Name + "(" + subProduct.Product.Name + ")";
                    int totalNotifications = 0;
                    foreach (var clientUser in clientUsers)
                    {
                        _context.Notifications.Add(
                            new Notification { Content = content, RecipientId = clientUser.Id, NotificationTypeId = notifificationTypeId }
                        );
                        totalNotifications++;
                    }
                    if (totalNotifications > 0)
                    {
                        await _context.SaveChangesAsync();
                        var userIds = clientUsers.Select(a => a.Id).ToList().ConvertAll(ident => ident.ToString());
                        var notifications = await _context.Notifications.Include(a => a.Recipient).Where(a => a.Recipient.HaKaDocClientId == hakaDocClientId).ToListAsync();
                        await _stockHubContext.Clients.All.SendAsync("StockAlert", userIds, notifications);
                    }
                }

            }
        }

        public async Task<List<SubProduct>> GetStoreSubProducts(int storeId, int haKaDocClientId, int productGroupId)
        {

            // var subProductIds = await _context.StoreProducts.Where(a => a.StoreId == storeId && a.SubProductId!=null).Select(a => a.SubProductId).ToListAsync();
            List<int> subProductIds = new List<int>();
            var storeProducts = await _context.StoreProducts.Include(a => a.SubProductSN).Where(a => a.StoreId == storeId).ToListAsync();
            subProductIds.AddRange(storeProducts.Where(s => s.SubProductId != null).Select(a => Convert.ToInt32(a.SubProductId)).ToList());
            var subproductIdWithSn = storeProducts.Where(a => a.SubProductSNId != null).ToList();
            if (subproductIdWithSn.Count() > 0)
            {
                var ids = new List<int>();
                foreach (var item in subproductIdWithSn)
                {
                    ids.Add(item.SubProductSN.SubProductId);
                }
                var idsToAdd = ids.Distinct();
                subProductIds.AddRange(idsToAdd);
            }
            subProductIds.AddRange(subproductIdWithSn.Select(a => a.Id).Distinct());


            var prods = await _context.SubProducts.Include(a => a.Photos)
                                           .Include(a => a.Product)
                                           .ThenInclude(a => a.Category)
                                           .Where(a => a.HaKaDocClientId == haKaDocClientId && subProductIds.Contains(a.Id) && a.Product.Category.ProductGroupId == productGroupId)
                                           .OrderBy(a => a.Id)
                                           .ToListAsync();
            return prods;

        }

        public async Task<List<SubProductSN>> GetStoreSubProductSnBySubProductId(int storeId, int subProductId)
        {
            var storeProductSnIds = await _context.StoreProducts.Where(a => a.StoreId == storeId).Select(a => a.SubProductSNId).ToListAsync();
            return await _context.SubProductSNs.Include(a => a.SubProduct)
                                               .ThenInclude(a => a.Photos)
                                               .Include(a => a.SubProduct.Product)
                                               .Include(a => a.SubProduct.Product.Category)
                                               .Where(a => storeProductSnIds.Contains(a.Id)).ToListAsync();


        }

        // public async Task<InventOpSubProductSN> InventOpSubProductSN(int inventOpId, int subPorductSNId)
        // {
        //     return  await _context.InventOpSubProductSNs.Include(a => a.InventOp).FirstOrDefaultAsync(a => a.InventOpId==inventOpId && a.SubProductSNId==subPorductSNId);
        // }
    }
}