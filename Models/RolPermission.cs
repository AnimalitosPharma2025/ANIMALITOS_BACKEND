using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class RolPermission
{
    public int Id { get; set; }

    public int RolId { get; set; }

    public int PermissionId { get; set; }

    public int StatusId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Permission Rol { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
