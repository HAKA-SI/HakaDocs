namespace API.Entities
{
    public class Doc : BaseEntity
    {
         public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int DocTypeId { get; set; }
        public DocType DocType { get; set; }
        public int InsertUserId { get; set; }
        public AppUser InsertUser { get; set; }
    }
}