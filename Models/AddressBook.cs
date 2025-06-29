using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class AddressBook
{
    public int Id { get; set; }

    public string? Direction { get; set; }

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string? Rfc { get; set; }

    public int StatusId { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Vendor> Vendors { get; set; } = new List<Vendor>();
}
