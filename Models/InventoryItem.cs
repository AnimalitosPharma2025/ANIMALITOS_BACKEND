using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class InventoryItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int ProductLotId { get; set; }

    public string Code { get; set; } = null!;

    public int? EmployeeId { get; set; }

    public int StatusId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual Status Status { get; set; } = null!;
}
