public class UserPermission
{
    public int Id { get; set; } = 0;

    public int UserId { get; set; } = 0;

    public int PermissionId { get; set; } = 0;

    public int StatusId { get; set; } = 0;
}

public class UserPermissionFilter
{
    public int Id { get; set; } = 0;

    public int UserId { get; set; } = 0;

    public int PermissionId { get; set; } = 0;

    public int StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}