using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Rol { get; set; }

    public int? AddressId { get; set; }

    public int StatusId { get; set; }

    public virtual AddressBook? Address { get; set; }

    public virtual ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
