namespace API.Entities
{
    public class ProductType
    {
         public int Id { get; set; }
        public int? ProductTypePId { get; set; }
        public ProductType ProductTypeP { get; set; }
        public string Name { get; set; }
          public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }
    }
}