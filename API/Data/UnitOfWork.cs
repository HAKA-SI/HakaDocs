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
