using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class Permission
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int StatusId { get; set; }

    public virtual ICollection<RolPermission> RolPermissionPermissions { get; set; } = new List<RolPermission>();

    public virtual ICollection<RolPermission> RolPermissionRols { get; set; } = new List<RolPermission>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}
