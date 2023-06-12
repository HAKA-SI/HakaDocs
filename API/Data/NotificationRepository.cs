using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _context;
        public NotificationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Notification>> AllClientUsersNotifications(List<int> userIds)
        {
             return await _context.Notifications.Include(a => a.NotificationType)
                                                 .OrderByDescending(a => a.InsertDate)
                                                 .Where(a =>userIds.Contains(a.RecipientId) && a.Deleted==false).ToListAsync();
        }

        public async Task<bool> DeleteUserNotifications(int userId, List<int> notificationIds)
        {
            var notifications = await _context.Notifications.Where(a => notificationIds.Contains(a.Id)).ToListAsync();
            int t=0;
            foreach (var item in notifications)
            {
               if(item.RecipientId!=userId) break;
                item.Deleted = true;
                item.DateDeleted = DateTime.Now;
                t++;
            }

            if(t==0) return true;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                
                return false;
            }
        }

        public async Task<bool> MarkNotificationsAsRead(int userId, List<int> ids)
        {
            var notifications = await _context.Notifications.Where(a => ids.Contains(a.Id)).ToListAsync();
            int t=0;
            foreach (var item in notifications)
            {
               if(item.RecipientId!=userId) break;
                item.Read = true;
                item.DateRead = DateTime.Now;
                t++;
            }

            if(t==0) return true;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception)
            {
                
                return false;
            }
        }

        public async Task<List<Notification>> UserNotificationTread(int userId)
        {
            return await _context.Notifications.Include(a => a.NotificationType)
                                                 .OrderByDescending(a => a.InsertDate)
                                                 .Where(a => a.RecipientId == userId && a.Deleted==false).ToListAsync();
        }


        
    }
}