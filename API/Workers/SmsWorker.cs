using System;
using System.Threading;
using System.Threading.Tasks;
using API.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Workers
{
    public class SmsWorker : BackgroundService
    {
        private readonly ILogger<SmsWorker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SmsWorker(ILogger<SmsWorker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    try
                    {

                        var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                        
              //          var clients = await dataContext.Clients.ToListAsync();
                
                    }
                    catch (System.Exception ex)
                    {

                        _logger.LogError(ex.Message);
                    }
                }

                _logger.LogInformation("Sms Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

    }

}