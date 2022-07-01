namespace API.Entities
{
    public class PaymentType
    {
         public PaymentType()
    {
      DsplSeq = 0;
      Active = true;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public byte DsplSeq { get; set; }
    public Boolean Active { get; set; }
    }
}