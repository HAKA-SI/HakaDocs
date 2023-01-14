using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Category>> GetCategories(int haKaDocClientId)
        {
            return await _context.Categories.AsNoTracking().Where(a => a.HaKaDocClientId == haKaDocClientId).OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<List<Category>> GetCategoriesWithProducts(int haKaDocClientId)
        {
            return await _context.Categories.Include(a => a.HaKaDocClient).Include(a => a.Products).Where(a => a.HaKaDocClientId == haKaDocClientId).OrderBy(a => a.Name).ToListAsync();
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

        public async Task<List<Product>> GetProductsWithDetails(int hakaDocClientId)
        {
            return await _context.Products.Include(a => a.Category).Where(a => a.HaKaDocClientId == hakaDocClientId).ToListAsync();
        }

    }
}