using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class NotificationsUser
{
    public int Id { get; set; }

    public bool? IsRead { get; set; }

    public int? UserId { get; set; }

    public int? StatusId { get; set; }

    public virtual Status? Status { get; set; }

    public virtual User? User { get; set; }
}
