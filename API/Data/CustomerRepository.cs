using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;
          public CustomerRepository(DataContext context)
        {
              _context = context;
        }

        public async Task<Customer> GetCustomerById(int customerId)
        {
            return await _context.Customers.FirstOrDefaultAsync(a => a.Id==customerId);
        }

        public async Task<List<Customer>> GetCustomerList(int haKaDocClientId)
        {
           var customers= await _context.Customers.Where(a => a.HaKaDocClientId==haKaDocClientId)
                                        .Include(a => a.MaritalSatus)
                                        .Include(a => a.City)
                                        .Include(a => a.Country)
                                        .Include(a => a.District)
                                        .Include(a => a.BirthCountry)
                                        .Include(a => a.MaritalSatus)
                                        .Include(a => a.BirthCity)
                                        .Include(a => a.MaritalSatus)
                                        .Include(a => a.BirthDistrict)
                                        .Include(a => a.HaKaDocClient)
                                        .ToListAsync();
           return customers;
        }
    }
}