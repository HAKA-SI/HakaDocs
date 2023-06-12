using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface INotificationRepository
    {
       Task<List<Notification>> AllClientUsersNotifications(List<int> userIds);
        Task<List<Notification>> UserNotificationTread(int userId);
        Task<bool> DeleteUserNotifications(int userId,List<int> notificationIds);
        Task<bool> MarkNotificationsAsRead(int userId, List<int> ids);
    }
}