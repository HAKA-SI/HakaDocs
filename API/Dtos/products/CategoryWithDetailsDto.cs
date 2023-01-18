using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Dtos
{
    public class CategoryWithDetailsDto:BaseEntityWithName
    {
           public string HaKaDocClient { get; set; }
            public int HaKaDocClientId { get; set; }
            public int TotalProducts { get; set; } = 0;
            public int ProductGroupId { get; set; }
        
    }
}