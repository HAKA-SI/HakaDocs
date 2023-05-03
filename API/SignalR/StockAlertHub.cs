using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using API.Entities;

namespace API.SignalR
{
    [Authorize]
    public class StockAlertHub: Hub
    {

        public async Task SendNotifications(List<string> userIds,List<Notification> notifications) 
        {
          await Clients.Users(userIds).SendAsync("StockAlert",notifications);
        }
        
    }
}