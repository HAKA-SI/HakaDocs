
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using API.SignalR;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly IEmailSender _sender;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHubContext<StockAlertHub> _stockHubContext;



        private readonly IMapper _mapper;

        public UnitOfWork(
            DataContext context,
            IMapper mapper,
            IEmailSender sender,
              SignInManager<AppUser> signInManager,
               IHubContext<StockAlertHub> stockHubContext,
               IConfiguration config
        )
        {
            _mapper = mapper;
            _context = context;
            _config = config;
            _sender = sender;
            _stockHubContext = stockHubContext;
            _signInManager = signInManager;
        }

        public IUserRepository UserRepository =>
            new UserRepository(_context, _mapper);

        public ICommRepository CommRepository => new CommRepository(_sender);
        public IAuthRepository AuthRepository => new AuthRepository(_signInManager);
        public ICustomerRepository CustomerRepository => new CustomerRepository(_context);
        public IProductRepository ProductRepository => new ProductRepository(_context,_config,_stockHubContext);
        public IStoreRepository StoreRepository => new StoreRepository(_context);


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

        public DataContext GetDataContext()
        {
            return _context;
        }
    }
}
