namespace ANIMALITOS_PHARMA_API.Models;

public partial class UserPermission
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PermissionId { get; set; }

    public int StatusId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
