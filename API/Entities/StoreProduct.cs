using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class StoreProduct:BaseEntity
    {
      public int StoreId { get; set; }
        public Store  Store { get; set; }
        public int? SubProductId { get; set; }
        public SubProduct SubProduct { get; set; }
        public int? SubProductSNId { get; set; }
        public SubProductSN SubProductSN { get; set; }
        public int Quantity { get; set; }
    }
}