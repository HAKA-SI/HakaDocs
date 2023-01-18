using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Dtos
{
     public class StoreListDto:BaseEntityWithName
    {
          public int? UserId { get; set; }
       // public AppUser User { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
    }
}