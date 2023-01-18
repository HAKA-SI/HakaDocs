using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IStoreRepository
    {
          Task<List<Store>> StoreList(int HaKaDocClientId);
    //   Task<Store> CreateStore(Store store);
    //   Task<List<SubProductSN>> StoreSubProductSNs(int storeId);
    }
}