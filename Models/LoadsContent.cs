using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class LoadsContent
{
    public int Id { get; set; }

    public int? LoadId { get; set; }

    public int? InventoryId { get; set; }

    public int? StatusId { get; set; }

    public virtual InventoryItem? Inventory { get; set; }

    public virtual Load? Load { get; set; }

    public virtual Status? Status { get; set; }
}
