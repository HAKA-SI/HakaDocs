using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class HaKaDocClient:BaseEntity
    {
        public string CountryCode { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string WebSiteUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
    }
}