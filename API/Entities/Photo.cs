using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Photo : BaseEntity
    {
        public int? UserId { get; set; }
        public AppUser User { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public bool IsApproved { get; set; }
        public int? SubProductId { get; set; }
        public SubProduct SubProduct { get; set; }
    }
}