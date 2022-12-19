namespace API.Entities
{
    public class Discount : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int? DiscountTypeId { get; set; }
        public DiscountType DiscountType { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public int? DiscountProductTypeId { get; set; }
        public ProductType DiscountProductType { get; set; }
        public decimal DiscountPercent { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0;
        public int ProductQtyMin { get; set; } = 0;
        public decimal AmountMin { get; set; } = 0;
         public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }
    }
}