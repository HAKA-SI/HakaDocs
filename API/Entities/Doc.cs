namespace API.Entities
{
    public class Doc : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int DocTypeId { get; set; }
        public DocType DocType { get; set; }
    }
}