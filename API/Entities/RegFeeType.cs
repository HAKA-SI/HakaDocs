namespace API.Entities
{
    public class RegFeeType
    {
        public int Id { get; set; }
        public int? RegFeeTypePId { get; set; }
        public RegFeeType RegFeeTypeP { get; set; }
        public string Name { get; set; }
    }
}