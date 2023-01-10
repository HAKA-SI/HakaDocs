namespace API.Entities
{
    public class BaseEntityWithName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }=true;
    }
}