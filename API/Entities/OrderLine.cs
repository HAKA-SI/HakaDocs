namespace API.Entities
{
    public class OrderLine
    {
        public OrderLine()
    {
      Qty = 1;
      DiscountAmount = 0;
      TVA = 0;
      Validated = false;
      Paid = false;
      Expired = false;
      OverDue = false;
      Cancelled = false;
      Active = false;
      Completed = false;
      InsertDate = DateTime.Now;
      InsertUserId = 1;
      UpdateDate = DateTime.Now;
      UpdateUserId = 1;
    }

    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public string OrderLineLabel { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public decimal ProductFee { get; set; }
    public int Qty { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalHT { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal AmountHT { get; set; }
    public decimal TVA { get; set; }
    public decimal TVAAmount { get; set; }
    public decimal AmountTTC { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime Validity { get; set; }
    public int? ChildId { get; set; }
    public User Child { get; set; }
    public Boolean Validated { get; set; }
    public Boolean Paid { get; set; }
    public Boolean Expired { get; set; }
    public Boolean OverDue { get; set; }
    public Boolean Cancelled { get; set; }
    public Boolean Completed { get; set; }
    public Boolean Active { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateUserId { get; set; }
    }
}