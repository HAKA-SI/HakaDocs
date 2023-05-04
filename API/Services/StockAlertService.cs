using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class StockAlertService
    {
        // private readonly DataContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<StockAlertService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<StockAlertHub> _stockHubContext;



        public StockAlertService(
             // DataContext context,
             IConfiguration config, ILogger<StockAlertService> logger, IServiceProvider serviceProvider, IHubContext<StockAlertHub> stockHubContext)
        {
            // _context = context;
            _config = config;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _stockHubContext = stockHubContext;
        }

        public async Task SendStockNotification(List<int> subproductIds, int hakaDocClientId)
        {
            _logger.LogInformation("debut service");
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                try
                {

                    /* This code is responsible for saving and sending a stock alert notification to all users of a specific
                    client when the quantity of a subproduct reaches its reorder level. */


                    int notifificationTypeId = _config.GetValue<int>("AppSettings:notificationType:stockAlertTypeId");

                    var clientUsers = await dbContext.Users.Where(a => a.HaKaDocClientId == hakaDocClientId).ToListAsync();
                    foreach (var item in subproductIds)
                    {

                        var subProduct = await dbContext.SubProducts.Include(a => a.Product).FirstOrDefaultAsync(a => a.Id == item);
                        if (subProduct.Quantity <= subProduct.ReorderLevel)//critical stock reached
                        {
                            var content = "<b>Alerte stock:</b> Vous avez atteint votre stock critique pour <b>" + subProduct.Name + "(" + subProduct.Product.Name + ")";
                            int totalNotifications = 0;
                            foreach (var clientUser in clientUsers)
                            {
                                dbContext.Notifications.Add(
                                    new Notification { Content = content, RecipientId = clientUser.Id, NotificationTypeId = notifificationTypeId }
                                );
                                totalNotifications++;
                            }
                            if (totalNotifications > 0)
                            {
                                await dbContext.SaveChangesAsync();
                                var userIds = clientUsers.Select(a => a.Id).ToList().ConvertAll(ident => ident.ToString());
                                var notifications = await dbContext.Notifications.Include(a => a.Recipient).Where(a => a.Recipient.HaKaDocClientId == hakaDocClientId).ToListAsync();
                                // await _stockHubContext.Clients.All.SendAsync("StockAlert", userIds, notifications);
                            }
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
    }
}