
using System.Threading.Tasks;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        private readonly IEmailSender _sender;

        private readonly IMapper _mapper;

        public UnitOfWork(
            DataContext context,
            IMapper mapper,
            IEmailSender sender
        )
        {
            _mapper = mapper;
            _context = context;
            _sender = sender;
        }

        public IUserRepository UserRepository =>
            new UserRepository(_context, _mapper);

        public ICommRepository CommRepository => new CommRepository(_sender);


        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async void AddAsync<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteAll<T>(List<T> entities) where T : class
        {
            _context.RemoveRange(entities);
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
