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

namespace API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Category>> GetCategories(int haKaDocClientId, int productGroupId)
        {
            return await _context.Categories.AsNoTracking().Where(a => a.HaKaDocClientId == haKaDocClientId && a.ProductGroupId==productGroupId).OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesWithProducts(int haKaDocClientId, int productGroupId)
        {
            return await _context.Categories.Include(a => a.HaKaDocClient).Include(a => a.Products).Where(a => a.HaKaDocClientId == haKaDocClientId && a.ProductGroupId==productGroupId).OrderBy(a => a.Name).ToListAsync();
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

        public async Task<List<Product>> GetProductsWithDetails(int hakaDocClientId,int productGroupId)
        {
            return await _context.Products.Include(a => a.Category).Where(a => a.HaKaDocClientId == hakaDocClientId && a.Category.ProductGroupId==productGroupId).ToListAsync();
        }

        public async Task<List<SubProduct>> GetSubProducts(int haKaDocClientId, int productGroupId)
        {
           return await _context.SubProducts.Include(a => a.Photos)
                                            .Include(a => a.Product)
                                            .ThenInclude(a => a.Category)
                                            .Where(a => a.HaKaDocClientId==haKaDocClientId && a.Product.Category.ProductGroupId==productGroupId)
                                            .ToListAsync();
        }

        public async Task<SubProduct> GetSubProduct(int subProductId)
        {
             return await _context.SubProducts.Include(a => a.Photos)
                                              .Include(a => a.Product)
                                              .ThenInclude(a => a.Category)
                                              .FirstOrDefaultAsync(a => a.Id ==subProductId);
        }

        public async Task<List<SubProduct>> GetSubProductWithSNs(int hakaDocClientId, int productGroupId)
        {
            return await _context.SubProducts.Include(a => a.Photos)
                                            .Include(a => a.Product)
                                            .ThenInclude(a => a.Category)
                                            .Where(a => a.HaKaDocClientId==hakaDocClientId &&a.WithSerialNumber==true && a.Product.Category.ProductGroupId==productGroupId)
                                            .ToListAsync();
        }

        public async Task<List<SubProduct>> GetSubProductWithoutSNs(int hakaDocClientId, int productGroupId)
        {
            return await _context.SubProducts.Include(a => a.Photos)
                                            .Include(a => a.Product)
                                            .ThenInclude(a => a.Category)
                                            .Where(a => a.HaKaDocClientId==hakaDocClientId &&a.WithSerialNumber==false && a.Product.Category.ProductGroupId==productGroupId)
                                            .ToListAsync();
        }

        public async Task<StockHistory> StoreSubProductHistory(int storeId, int subProductId)
        {
           return await _context.StockHistories.FirstOrDefaultAsync(a => a.StoreId == storeId && a.SubProductId == subProductId);
        }

        public async Task<StoreProduct> StoreProduct(int storeId, int subProductId)
        {
           return await _context.StoreProducts.FirstOrDefaultAsync(a =>a.StoreId==storeId && a.SubProductId==subProductId);
        }

        public async  Task<List<SubProductSN>> GetSubProductSnBySubProductId(int subProductId)
        {
            return await _context.SubProductSNs.Include(a => a.SubProduct)
                                                .ThenInclude(a =>a.Photos)
                                                .Include(a =>a.SubProduct.Product)
                                                .Include(a => a.SubProduct.Product.Category)
                                                .Where(a => a.SubProductId == subProductId).ToListAsync();
        }
    }
}