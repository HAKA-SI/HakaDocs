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

        public async Task<List<Notification>> UserNotificationTread(int userId)
        {
           return await _context.Notifications.Where(a => a.RecipientId==userId).ToListAsync();
        }
    }
}