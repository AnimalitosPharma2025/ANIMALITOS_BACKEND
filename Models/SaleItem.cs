using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class SaleItem
{
    public int Id { get; set; }

    public int SaleId { get; set; }

    public int InventoryId { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? Discount { get; set; }

    public virtual InventoryItem Inventory { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
