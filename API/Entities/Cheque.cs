namespace API.Entities
{
    public class Cheque
    {
        public int Id { get; set; }
    public int? BankId { get; set; }
    public Bank Bank { get; set; }
    public string ChequeNum { get; set; }
    public decimal Amount { get; set; }
    public string PictureUrl { get; set; }
     public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }
    }
}