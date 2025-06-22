public class Status
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "";

    public string? Description { get; set; } = "";
}

public class StatusFilter
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "";

    public string? Description { get; set; } = "";

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}