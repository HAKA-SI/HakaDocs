namespace API.Entities
{
    public class Receipt:BaseEntity
    {
         public string Num { get; set; }
        public DateTime ReceiptDate { get; set; }
    }
}