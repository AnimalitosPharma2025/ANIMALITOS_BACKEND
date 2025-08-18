namespace ANIMALITOS_PHARMA_API.Contracts;
public class Notification
{
    public int Id { get; set; } = 0;

    public string? Title { get; set; } = "";

    public string? Message { get; set; } = "";

    public int? StatusId { get; set; } = 0;
}

public class NotificationFilter
{
    public int Id { get; set; } = 0;

    public string? Title { get; set; } = "";

    public string? Message { get; set; } = "";

    public int? StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}
