using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class UserRol
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public virtual Rol Rol { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
