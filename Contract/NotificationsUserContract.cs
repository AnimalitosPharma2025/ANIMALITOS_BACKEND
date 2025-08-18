namespace ANIMALITOS_PHARMA_API.Contracts;

public class NotificationsUser
{
    public int Id { get; set; } = 0;

    public bool? IsRead { get; set; } = false;

    public int? UserId { get; set; } = 0;

    public int? StatusId { get; set; } = 0;
}

public class NotificationsUserFilter
{
    public int Id { get; set; } = 0;

    public bool? IsRead { get; set; } = false;

    public int? UserId { get; set; } = 0;

    public int? StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}