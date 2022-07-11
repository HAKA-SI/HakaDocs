
namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository{get;}
        ICommRepository CommRepository{get;}
        Task<bool> Complete();
        bool HasChanges();
    }
}