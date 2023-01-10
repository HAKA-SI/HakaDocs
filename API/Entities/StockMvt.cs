namespace API.Entities
{
    public class StockMvt:BaseEntity
    {
        public DateTime MvtDate { get; set; }
        public string RefNum { get; set; }
        public string Note { get; set; }
        public int? FromStoreId { get; set; }
        public int? FromEmployeeId { get; set; }
        public int? ToStoreId { get; set; }
        public int? ToEmployeeId { get; set; }
        public byte Status { get; set; } =1;

        public  AppUser FromEmployee { get; set; }
        public  Store FromStore { get; set; }
        public int InventOpTypeId { get; set; }
        public  InventOpType InventOpType { get; set; }
        public  AppUser ToEmployee { get; set; }
        public  Store ToStore { get; set; }
        public int InsertUserId { get; set; }
        public AppUser InsertUser { get; set; }
    }
}