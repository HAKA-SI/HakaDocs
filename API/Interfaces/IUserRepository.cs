using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<List<AppUser>> GetClientUsers(int hakaDocClientId);
        Task<AppUser> GetUserByIdAsync(int id);
    }
}