using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class ProductLot
{
    public int Id { get; set; }

    public DateTime Expiration { get; set; }

    public DateTime DateReceipt { get; set; }

    public int StatusId { get; set; }

    public virtual Status Status { get; set; } = null!;
}
