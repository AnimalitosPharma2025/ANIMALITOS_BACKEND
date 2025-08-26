using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Credit
{
    public int Id { get; set; }

    public DateTime PurchaseDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public int StatusId { get; set; }

    public bool? AuthorizeCredit { get; set; }

    public int? ClientId { get; set; }

    public double? TotalDebt { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Status Status { get; set; } = null!;
}
