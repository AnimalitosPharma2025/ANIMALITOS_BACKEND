public class InventoryItem
{
    public int Id { get; set; } = 0;

    public int ProductId { get; set; } = 0;

    public int ProductLotId { get; set; } = 0;

    public string Code { get; set; } = null!;

    public int? EmployeeId { get; set; } = 0;

    public int StatusId { get; set; } = 0;
}

public class InventoryItemFilter
{
    public int Id { get; set; } = 0;

    public int ProductId { get; set; } = 0;

    public int ProductLotId { get; set; } = 0;

    public string Code { get; set; } = null!;

    public int? EmployeeId { get; set; } = 0;

    public int StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}