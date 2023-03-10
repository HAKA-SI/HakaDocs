using System;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Workers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Sms Worker start....");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // using (var scope = _serviceScopeFactory.CreateScope())
                    // {
                    //     //     var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    //     //    var connectionString = builder["ConnectionStrings:DefaultConnection"];
                    //     //     var optionsBuilder = new DbContextOptionsBuilder();
                    //     //     optionsBuilder.UseSqlServer(connectionString);

                    //     //     var context = new DataContext(optionsBuilder.Options);

                    //     //     var users = await context.Users.ToListAsync();
                    //     //     foreach (var item in users)
                    //     //     {
                    //     //         item.Created = DateTime.Now;
                    //     //     }
                    //     //     await context.SaveChangesAsync();

                    //     var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                    //     var users = await dataContext.Users.ToListAsync();
                    //     foreach (var item in users)
                    //     {
                    //         item.Created = DateTime.Now;
                    //     }
                    //     await dataContext.SaveChangesAsync();
                    // }

                }
                catch (System.Exception)
                {
                    throw;
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}