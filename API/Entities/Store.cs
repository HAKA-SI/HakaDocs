using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Store:BaseEntity
    {
        public string Name { get; set; }
        public string Observation { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}