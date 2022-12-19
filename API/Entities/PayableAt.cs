namespace API.Entities
{
    public class PayableAt
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public int DayCount { get; set; }
          public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }
    }
}