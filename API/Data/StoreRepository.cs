using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DataContext _context;
        public StoreRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<InventOp>> GetStoreInventOps(int storeId)
        {
            return await _context.InventOps.Include(a => a.InventOpType)
                                                        .Include(a => a.FromStore)
                                                        .Include(a => a.ToStore)
                                                        .Include(a => a.FromEmployee)
                                                        .Include(a => a.ToEmployee)
                                                        .Include(a => a.SubProduct)
                                                        .ThenInclude(a =>a.Product)
                                                        .ThenInclude(a =>a.Category)
                                                        .Include(a => a.SubProduct.Photos)
                                                        .Include(a => a.InsertUser)
                                                        .Where(a =>a.FromStoreId == storeId || a.ToStoreId==storeId)
                                                        .OrderByDescending(a =>a.OpDate)
                                                        .ToListAsync();
        }

        public async Task<StoreProduct> GetStoreProduct(int storeId, int subProductId)
        {
            return await _context.StoreProducts.FirstOrDefaultAsync(a =>a.StoreId==storeId && a.SubProductId==subProductId);
        }

        public async Task<List<SubProduct>> GetStoreStock(int storeId)
        {
            var storeProduct = await _context.StoreProducts.Where(a => a.StoreId == storeId).ToListAsync();
            var productIds = storeProduct.Select(a => a.SubProductId).ToList();
            return await _context.SubProducts.Include(a => a.Photos)
                                            .Include(a =>a.Product)
                                            .ThenInclude(a => a.Category)
                                            .Where(a => productIds.Contains(a.Id)).ToListAsync();
        }

        public async Task<List<SubProduct>> GetStoreStockElements(int storeId)
        {
            var subproducts = await _context.StoreProducts.Include(s => s.SubProduct)
                                                            .ThenInclude(a => a.Product)
                                                            .ThenInclude(a => a.Category)
                                                            .Where(a => a.StoreId == storeId).ToListAsync();
            return subproducts.Select(a => a.SubProduct).ToList();
        }

        public async Task<List<Store>> StoreList(int HaKaDocClientId)
        {
            return await _context.Stores.Include(a => a.District).Where(a => a.HaKaDocClientId == HaKaDocClientId).ToListAsync();
        }
    }
}