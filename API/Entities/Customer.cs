using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Customer: BaseEntity
    {
         public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }
        
    }
}