namespace API.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public Type Type { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

       // public ICollection<Photo> Photos{ get; set; }
    }
}