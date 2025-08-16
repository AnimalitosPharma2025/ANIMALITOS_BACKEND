using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Notification
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public int? StatusId { get; set; }

    public virtual Status? Status { get; set; }
}
