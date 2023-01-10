namespace API.Entities
{
    public class StockMvtInventOp
    {
        public int Id { get; set; }
        public int StockMvtId { get; set; }
        public StockMvt StockMvt { get; set; }
        public InventOp InventOp { get; set; }
        public int InventOpId { get; set; }
    }
}