using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class InvoiceOrderLine: BaseEntity
    {
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int OrderLineId { get; set; }       
        public OrderLine OrderLine { get; set; }       
    }
}