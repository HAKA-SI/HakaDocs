namespace API.Entities
{
    public class DeadLine
    {
        public DeadLine()
        {
            InsertDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime InsertDate { get; set; }
         public HaKaDocClient HaKaDocClient { get; set; }
        public int HaKaDocClientId { get; set; }

    }
}