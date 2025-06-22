namespace ANIMALITOS_PHARMA_API.Models;

public partial class Vendor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? AddressId { get; set; }

    public int StatusId { get; set; }

    public virtual AddressBook? Address { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Status Status { get; set; } = null!;
}
