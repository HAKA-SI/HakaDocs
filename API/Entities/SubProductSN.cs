namespace API.Entities
{
    public class SubProductSN: BaseEntityWithName
    {
        public Store Store { get; set; }
        public int StoreId { get; set; }
        public int SubProductId { get; set; }
        public SubProduct SubProduct { get; set; }
        public string SN { get; set; }
        public int Quantity { get; set; }=1;
     //   public ICollection<Photo> Photos{ get; set; }

    }
}