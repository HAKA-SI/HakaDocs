namespace API.Entities
{
    public class DiscountType
    {
        public int Id { get; set; }
        public int? DiscountTypePId { get; set; }
        public DiscountType DiscountTypeP { get; set; }
        public string Name { get; set; }
    }
}