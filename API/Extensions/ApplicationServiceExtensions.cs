
// using API.workers;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddSingleton<PresenceTracker>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<LogUserActivity>();
            //   services.AddHostedService<Worker>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            },
            ServiceLifetime.Transient);

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddMemoryCache();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.Configure<AuthMessageSenderOptions>(config);
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));


            return services;
        }
    }
}