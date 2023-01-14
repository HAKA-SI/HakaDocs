namespace API.Entities
{
    public class Category:BaseEntityWithName
    {
            public HaKaDocClient HaKaDocClient { get; set; }
            public int HaKaDocClientId { get; set; }
            public AppUser InsertUser { get; set; }
            public int InsertUserId { get; set; }
            public AppUser UpdateUser { get; set; }
            public int? UpdateUserId { get; set; }
            public ICollection<Product> Products { get; set; }
    }
}