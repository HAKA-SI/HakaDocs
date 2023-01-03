using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppRole: IdentityRole<int>
    {
         public HaKaDocClient HaKaDocClient { get; set; }
        public int? HaKaDocClientId { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}