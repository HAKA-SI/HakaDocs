namespace API.Entities
{
    public class SubProductFeature
    {
        public int Id { get; set; }
        public SubProduct SubProduct { get; set; }
        public int SubProductId { get; set; }
        public Feature Feature { get; set; }
        public int FeatureId { get; set; }
    }
}