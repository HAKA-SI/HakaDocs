using System;

namespace API.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        // public string Version { get; set; }
        public bool Active { get; set; } = true;
    }
}