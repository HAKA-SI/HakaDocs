using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public City City { get; set; }
        public int? CityId { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<Email> EmailsSent { get; set; }
        public ICollection<Email> EmailsReceived { get; set; }
        public HaKaDocClient HaKaDocClient { get; set; }
        public int? HaKaDocClientId { get; set; }
        public  string ValidationCode { get; set; }
        public DateTime? ValidationDate { get; set; }
        public bool CodeValidated { get; set; }
        public bool Active { get; set; } = false;


    }
}