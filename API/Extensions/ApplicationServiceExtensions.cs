
// using API.workers;

using API.Core.Interfaces;
using API.Core.services;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using API.SignalR;
using API.Workers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using WhatsappBusiness.CloudApi.Configurations;
using WhatsappBusiness.CloudApi.Extensions;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            WhatsAppBusinessCloudApiConfig whatsAppConfig = new WhatsAppBusinessCloudApiConfig();
            whatsAppConfig.WhatsAppBusinessPhoneNumberId ="118469731225091";
            //  builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessPhoneNumberId"];
            whatsAppConfig.WhatsAppBusinessAccountId ="116280854779357";
            //  builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessAccountId"];
            whatsAppConfig.WhatsAppBusinessId = "254028866994762";
            // builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["WhatsAppBusinessId"];
            whatsAppConfig.AccessToken ="EAADnCbRhQkoBAM6wZBGZCy1f7KwXLZCWfKb0IDDeoJvsSYRdvTfREvHcm02QeILDHSSvJg1tCDSmrZBv0jMoye6NKIFsnUnwyIa5h4qcWW5DLOlzc6SPwVtuBeCFDKwzDPGAsHBMmyqcZByQABHM6naG82WhUiKqsZCsClowTA064n9atljpxdni58psijDLRqI2AgRh1pAQZDZD";
            //  builder.Configuration.GetSection("WhatsAppBusinessCloudApiConfiguration")["AccessToken"];
            services.AddWhatsAppBusinessCloudApiService(whatsAppConfig);

            services.AddSingleton<PresenceTracker>();
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<LogUserActivity>();
            //   services.AddHostedService<Worker>();
            //   services.AddHostedService<TestWorker>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

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