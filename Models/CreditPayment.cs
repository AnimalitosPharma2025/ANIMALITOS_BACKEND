using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class CreditPayment
{
    public int Id { get; set; }

    public int CreditId { get; set; }

    public double PaymentAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public int? StatusId { get; set; }

    public virtual Status? Status { get; set; }
}
