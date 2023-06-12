using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using API.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Workers
{
    public class StockCheckingWorker : IHostedService, IDisposable
    {
        private readonly ILogger<StockCheckingWorker> _logger;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;
        private readonly CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        private Task _executingTask;

        public StockCheckingWorker(ILogger<StockCheckingWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("my test background service is starting");
            _executingTask = Task.Run(() => Run(_cancelTokenSource.Token));
            return Task.CompletedTask;
        }

        private async void Run(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var _context = scope.ServiceProvider.GetRequiredService<DataContext>();
                        try
                        {

                            // int notifificationTypeId = _config.GetValue<int>("AppSettings:notificationType:stockAlertTypeId");
                            int notifificationTypeId = 1;

                            var orderToCheck = await _context.Orders.Where(a => a.StockChecked == false).ToListAsync();
                            foreach (var order in orderToCheck)
                            {
                                var orderLines = await _context.OrderLines.Where(r => r.OrderId == order.Id).ToListAsync();
                                foreach (var orderLine in orderLines)
                                {
                                    var subProduct = await _context.SubProducts.Include(p =>p.Product).FirstOrDefaultAsync(p => p.Id == orderLine.SubProductId);
                                    if (subProduct.Quantity <= subProduct.ReorderLevel)
                                    {
                                        int totalNotifications = 0;
                                        var content = "<b>Alerte stock:</b> Vous avez atteint votre stock critique pour <b>" + subProduct.Name + "(" + subProduct.Product.Name + ")";


                                        //envoi de notification
                                        var clientUsers = await _context.Users.Where(a => a.HaKaDocClientId == subProduct.HaKaDocClientId).ToListAsync();
                                        foreach (var clientUser in clientUsers)
                                        {
                                            _context.Notifications.Add(
                                                                    new Notification { Content = content, RecipientId = clientUser.Id, NotificationTypeId = notifificationTypeId }
                                                                    );
                                            totalNotifications++;
                                        }
                                         if (totalNotifications > 0)
                                         {
                                            await _context.SaveChangesAsync();
                                            //
                                         }

                                    }

                                    order.StockChecked= true;
                                    await _context.SaveChangesAsync();
                                }
                            }


                        }
                        catch (System.Exception ex)
                        {
                            _logger.LogError(ex, ex.Message);
                            // _context.Logger.Error(ex.Message);
                            throw;

                        }



                    }

                }
                catch (System.Exception)
                {

                    _logger.LogInformation("an error occured while running my test background service");
                }
                Thread.Sleep(1000);

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("my test background serice is stopping.");
            if (_executingTask == null)
            {
                return Task.CompletedTask;
            }
            _cancelTokenSource.Cancel();
            return Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
        }

        public void Dispose()
        {
            _cancelTokenSource.Dispose();
        }

    }
}