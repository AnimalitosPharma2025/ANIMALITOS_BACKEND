using System;
using System.Collections.Generic;

namespace ANIMALITOS_PHARMA_API.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public int? EmployeeId { get; set; }

    public int StatusId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<NotificationsUser> NotificationsUsers { get; set; } = new List<NotificationsUser>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}
