using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double CreditLimit { get; set; }

    public int? AddressId { get; set; }

    public int StatusId { get; set; }

    public virtual AddressBook? Address { get; set; }

    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual Status Status { get; set; } = null!;
}
