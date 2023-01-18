using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class CustomerCode
    {
        public int Id { get; set; }
        public int HaKaDocClientId { get; set; }
        public HaKaDocClient HaKaDocClient { get; set; }
        public int CodeLevel { get; set; }
    }
}