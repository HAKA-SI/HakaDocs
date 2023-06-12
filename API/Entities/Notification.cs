using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Notification: BaseEntity
    {

        public int RecipientId { get; set; }
        public AppUser Recipient { get; set; }
        public string Content { get; set; }
        public Boolean Read { get; set; } = false;
        public Boolean Deleted { get; set; } = false;
         public DateTime? DateRead { get; set; }
         public DateTime? DateDeleted { get; set; }
         public NotificationType NotificationType { get; set; }
         public int NotificationTypeId { get; set; }
        
    }
}