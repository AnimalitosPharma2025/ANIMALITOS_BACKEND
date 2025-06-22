public class ProductLot
{
    public int Id { get; set; } = 0;

    public DateTime Expiration { get; set; } = DateTime.Now;

    public DateTime DateReceipt { get; set; } = DateTime.Now;

    public int StatusId { get; set; } = 0;
}

public class ProductLotFilter
{
    public int Id { get; set; } = 0;

    public DateTime Expiration { get; set; } = DateTime.Now;

    public DateTime DateReceipt { get; set; } = DateTime.Now;

    public int StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}