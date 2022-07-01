namespace API.Entities
{
    public class OrderLineDiscount
    {
         public int Id { get; set; }
        public int OrderLineId { get; set; }
        public OrderLine OrderLine { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
        public decimal Amount { get; set; }
    
    }
}