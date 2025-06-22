public class Rol
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public int StatusId { get; set; } = 0;
}

public class RolFilter
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public int StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}