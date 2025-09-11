using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class InventoryItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int ProductLotId { get; set; }

    public int? EmployeeId { get; set; }

    public int StatusId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<LoadsContent> LoadsContents { get; set; } = new List<LoadsContent>();

    public virtual ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

    public virtual Status Status { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ProductLot ProductLot { get; set; } = null!;
}
