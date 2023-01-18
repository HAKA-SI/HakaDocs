using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StoreRepository: IStoreRepository
    {
        private readonly DataContext _context;
        public StoreRepository(DataContext context)
        {
            _context = context;
        }

        
        public async Task<List<Store>> StoreList(int HaKaDocClientId)
        {
           return await _context.Stores.Include(a => a.District).Where(a =>a.HaKaDocClientId==HaKaDocClientId).ToListAsync();
        }
    }
}