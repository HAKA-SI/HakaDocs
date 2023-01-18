using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(int customerId);
        Task<CustomerCode> GetCustomerCodeLevel(int haKaDocClientId);
        Task<List<Customer>>GetCustomerList(int haKaDocClientId);
      
    }
}