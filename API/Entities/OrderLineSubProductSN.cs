using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class OrderLineSubProductSN:BaseEntity
    {
        public int SubProductSNId { get; set; }
        public SubProductSN SubProductSN { get; set; }
        public int OrderLineId { get; set; }
        public OrderLine OrderLine { get; set; }
        public decimal DiscountAmout { get; set; }=0;
    }
}