using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Category>> GetCategories(int haKaDocClientId);
        Task<List<Category>> GetCategoriesWithProducts(int haKaDocClientId);
        Task<Category> GetCategory(int categoryId);
        Task<Product> GetProduct(int productId);
        Task<Product> GetProductWithDetails(int productId);
        Task<List<Product>> GetProducts(int hakaDocClientId);
        Task<List<Product>> GetProductsWithDetails(int hakaDocClientId);
    }
}