using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int RecipientId { get; set; }
        // public AppUser Recipient { get; set; }
        public string Content { get; set; }
        public Boolean Read { get; set; } = false;
        public Boolean Deleted { get; set; } = false;
        public DateTime? DateRead { get; set; }
        public String NotificationType { get; set; }
        public int NotificationTypeId { get; set; }
    }
}