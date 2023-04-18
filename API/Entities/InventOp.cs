using System.Collections.Generic;

namespace API.Entities
{
    public class InventOp: BaseEntity
    {
        public int? InventOpTypeId { get; set; }
        public DateTime OpDate { get; set; }
        public int? FromStoreId { get; set; }
        public int? FromEmployeeId { get; set; }
        public int? ToStoreId { get; set; }
        public int? ToEmployeeId { get; set; }
         public string FormNum { get; set; }
        public byte Status { get; set; }
              public int? InsertUserId { get; set; }

        public  AppUser FromEmployee { get; set; }
        public  Store FromStore { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public  InventOpType InventOpType { get; set; }
        public  AppUser ToEmployee { get; set; }
         public  AppUser InsertUser { get; set; }
        public  Store ToStore { get; set; }
        public int? SubProductId { get; set; }
        public SubProduct SubProduct { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? Quantity { get; set; }
        public StockHistory StockHistory { get; set; }
        public ICollection<InventOpSubProductSN> InventOpSubProductSNs { get; set; }
        public ICollection<StockMvtInventOp> StockMvtInventOps { get; set; }
    }
}