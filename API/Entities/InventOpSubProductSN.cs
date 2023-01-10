using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class InventOpSubProductSN:BaseEntity
    {
      public int SubProductSNId { get; set; }
        public SubProductSN SubProductSN { get; set; }
        public int InventOpId { get; set; }
        public InventOp InventOp { get; set; }
    }
}