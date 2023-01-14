namespace API.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public Type Type { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }
          public AppUser InsertUser { get; set; }
            public int InsertUserId { get; set; }
            public AppUser UpdateUser { get; set; }
            public int? UpdateUserId { get; set; }

        // public ICollection<Photo> Photos{ get; set; }
    }
}