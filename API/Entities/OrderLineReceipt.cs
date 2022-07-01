namespace API.Entities
{
    public class OrderLineReceipt:BaseEntity
    {
         public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }
        public int OrderLineId { get; set; }
        public OrderLine OrderLine { get; set; }
    }
}