namespace API.Entities
{
    public class SubProduct:BaseEntity
    {
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public Type Type { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool Discontinued { get; set; }= false;
        public int UnitInStock { get; set; }=0;
        public int UnitPrice { get; set; }=0;
        public int QuantityPerUnite { get; set; }=1;
        public int UnitsOnOrder { get; set; }=0;
        public int ReorderLevel { get; set; }=0;
        public int Quantity { get; set; }=0;
        public bool WithSerialNumber { get; set; }
        public string Note { get; set; }

        public ICollection<Photo> Photos { get; set; }

    }
}