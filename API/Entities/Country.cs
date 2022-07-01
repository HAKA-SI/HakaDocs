namespace API.Entities
{
    public class Country: BaseEntity
    {
       public Country()
       {
        Active=false;
       }
        public string Name { get; set; }
        public string Code { get; set; }

    }
}