namespace API.Entities
{
    public class OrderLineRegFee
    {
        public int Id { get; set; }
        public int OrderLineId { get; set; }
        public OrderLine OrderLine { get; set; }
        public int RegFeeId { get; set; }
        public Product RegFee { get; set; }
        public decimal Amount { get; set; }
    }
}