using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class StockHistory: BaseEntity
    {
      public int UserId { get; set; }
      public AppUser User { get; set; }
      public DateTime OpDate { get; set; }
      public int InventOpId { get; set; }
      public InventOp InventOp { get; set; }
      public int StockHistoryActionId { get; set; }
        public StockHistoryAction StockHistoryAction { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int SubProductId { get; set; }
        public SubProduct SubProduct { get; set; }
        // public int? SubProductSNId { get; set; }
        // public SubProductSN SubProductSN { get; set; }
        public int OldQty { get; set; }
        public int NewQty { get; set; }
        public int Delta { get; set; } // Delta = newQty-OldQty
    }
}