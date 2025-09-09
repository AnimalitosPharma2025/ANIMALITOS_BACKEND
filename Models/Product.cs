using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Category { get; set; } = null!;

    public double PurchasePrice { get; set; }

    public double UnitPrice { get; set; }

    public int VendorId { get; set; }

    public int StatusId { get; set; }

    public string Code { get; set; }

    public string? ImageUrl { get; set; }

    public double? Discount { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;
}
