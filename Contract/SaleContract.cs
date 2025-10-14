public class Sale
{
    public int Id { get; set; } = 0;

    public DateTime PurchaseDate { get; set; } = DateTime.Now;

    public int? ClientId { get; set; } = 0;

    public int EmployeeId { get; set; } = 0;

    public decimal Total { get; set; }

    public int StatusId { get; set; } = 0;
}

public class SaleFilter
{
    public int Id { get; set; } = 0;

    public DateTime PurchaseDate { get; set; } = DateTime.Now;

    public int? ClientId { get; set; } = 0;

    public int EmployeeId { get; set; } = 0;

    public decimal Total { get; set; }

    public int StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}