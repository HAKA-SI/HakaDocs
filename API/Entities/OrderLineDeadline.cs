namespace API.Entities
{
    public class OrderLineDeadline
    {
        public OrderLineDeadline()
    {
      ProductFee = 0;
      InsertDate = DateTime.Now;
      InsertUserId = 1;
      UpdateDate = DateTime.Now;
      UpdateUserId = 1;
    }
    
    public int Id { get; set; }
    public int OrderLineId { get; set; }
    public OrderLine OrderLine { get; set; }
    public decimal Percent { get; set; }
    public decimal Amount { get; set; }
    public decimal ProductFee { get; set; }
    public DateTime DueDate { get; set; }
    public string DeadlineName { get; set; }
    public string Comment { get; set; }
    public byte Seq { get; set; }
    public Boolean Paid { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public AppUser InsertUser { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateUserId { get; set; }
    public AppUser UpdateUser { get; set; }
    }
}