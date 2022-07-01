
namespace API.Entities
{
  public class Capability:BaseEntity
  {
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }
    public byte AccessType { get; set; }
  }
}
