using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IStoreRepository
    {
        Task<List<InventOp>> GetStoreInventOps(int storeId);
        Task<StoreProduct> GetStoreProduct(int storeId, int subProductId);
        Task<List<SubProduct>> GetStoreStock(int storeId);
        Task<List<SubProduct>> GetStoreStockElements(int storeId);
        Task<List<Store>> StoreList(int HaKaDocClientId);
    //   Task<Store> CreateStore(Store store);
    //   Task<List<SubProductSN>> StoreSubProductSNs(int storeId);
    }
}