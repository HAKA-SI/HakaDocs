using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using API.Entities;
using API.Interfaces;
using API.Extensions;
using Microsoft.Extensions.Logging;

namespace API.SignalR
{
    [Authorize]
    public class StockAlertHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StockAlertHub> _logger;

        public StockAlertHub(IUnitOfWork unitOfWork,ILogger<StockAlertHub> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            // var httpContext = Context.GetHttpContext();
            // var userId = httpContext.Request.Query["userId"].ToString();

            _logger.LogInformation("debut connection");
            var notifications = await _unitOfWork.NotificationRepository.UserNotificationTread(Context.User.GetUserId());
            _logger.LogInformation("notifs",notifications);
          
            await Clients.Caller.SendAsync("NotificationsThread", notifications);
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // var group = await RemoveFromMessageGroup();
            // await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
            // await base.OnDisconnectedAsync(exception);
        }


        public async Task SendNotifications(List<string> userIds, List<Notification> notifications)
        {
            await Clients.Users(userIds).SendAsync("StockAlertsReceived", notifications);
        }

    }
}