using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Sale
{
    public int Id { get; set; }

    public DateTime PurchaseDate { get; set; }

    public int ClientId { get; set; }

    public int EmployeeId { get; set; }

    public int StatusId { get; set; }

    public decimal? Total { get; set; }

    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    public virtual Client? Client { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    //public virtual InventoryItem? Inventory { get; set; }

    public virtual Status Status { get; set; } = null!;
}
