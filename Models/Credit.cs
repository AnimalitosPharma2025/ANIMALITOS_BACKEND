using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Credit
{
    public int Id { get; set; }

    public DateTime PurchaseDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int SaleId { get; set; }

    public int StatusId { get; set; }

    public virtual Sale Sale { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
