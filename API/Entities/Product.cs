namespace API.Entities
{
    public class Product
    {
        public Product()
    {
      ServiceStartDate = new DateTime();
        IsRequired = false;
        IsPaidCash = true;
        IsPctOrAmount = false;
        IsByLevel = false;
        IsByZone = false;
        Active = true;
        DsplSeq = 0;
    }
    public int Id { get; set; }
    public int? ProductPId { get; set; }
    public Product ProductP { get; set; }
    public string Name { get; set; }
    public string Comment { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    public int? RegFeeTypeId { get; set; }
    public RegFeeType RegFeeType { get; set; }
    public int? PeriodicityId { get; set; }
    public Periodicity Periodicity { get; set; }
    public decimal? Price { get; set; }
    public bool IsPaidCash { get; set; }
    public bool IsPctOrAmount { get; set; }
    public bool IsByLevel { get; set; }
    public bool IsByZone { get; set; }
    public bool IsPeriodic { get; set; }
    public DateTime ServiceStartDate { get; set; }
    public int? PayableAtId { get; set; }
    public PayableAt PayableAt { get; set; }
    public bool IsRequired { get; set; }
    public bool Active { get; set; }
    public byte DsplSeq { get; set; }
    }
}