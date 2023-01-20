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