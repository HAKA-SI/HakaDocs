using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Category>> GetCategories(int haKaDocClientId, int productGroupId);
        Task<List<Category>> GetCategoriesWithProducts(int haKaDocClientId, int productGroupId);
        Task<Category> GetCategory(int categoryId);
        Task<Product> GetProduct(int productId);
        Task<Product> GetProductWithDetails(int productId);
        // Task<List<Product>> GetProducts(int hakaDocClientId);
        Task<List<Product>> GetProductsWithDetails(int hakaDocClientId,int productGroupId);
        Task<List<SubProduct>> GetSubProducts(int haKaDocClientId,int productGroupId);
        Task<SubProduct> GetSubProduct(int subProductId);
        Task<List<SubProduct>> GetSubProductWithSNs(int hakaDocClientId, int productGroupId);
        Task<List<SubProduct>> GetSubProductWithoutSNs(int hakaDocClientId, int productGroupId);
        Task<StockHistory> StoreSubProductHistory(int storeId, int subProductId);
        Task<StoreProduct> StoreProduct(int storeId, int subProductId);
    }
}