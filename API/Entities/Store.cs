namespace API.Entities
{
    public class Store : BaseEntityWithName
    {
        public int? UserId { get; set; }
        public AppUser User { get; set; }
        public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }
        public int? DistrictId { get; set; }
        public District District { get; set; }
    }
}