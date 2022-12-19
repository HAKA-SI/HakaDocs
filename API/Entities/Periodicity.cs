namespace API.Entities
{
    public class Periodicity
    {
        public Periodicity()
        {
            NbDays = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbrev { get; set; }
        public byte NbDays { get; set; }
          public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }
    }
}