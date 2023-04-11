using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class InvoiceTemplate: BaseEntity
    {
        public string Content { get; set; }
        public string Name { get; set; }
        public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }
    }
}