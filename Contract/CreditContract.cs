public class Credit
{
    public int Id { get; set; } = 0;

    public DateTime PurchaseDate { get; set; } = DateTime.Now;

    public DateTime ExpirationDate { get; set; } = DateTime.Now;

    public int StatusId { get; set; } = 0;

    public bool? AuthorizeCredit { get; set; } = false;

    public double? TotalDebt { get; set; } = 0;

    public int? ClientId { get; set; } = 0;
}

public class CreditFilter
{
    public int Id { get; set; } = 0;

    public DateTime? PurchaseDate { get; set; } = null;

    public DateTime? ExpirationDate { get; set; } = null;

    public int SaleId { get; set; } = 0;

    public int StatusId { get; set; } = 0;

    public bool? AuthorizeCredit { get; set; } = false;

    public double? TotalDebt { get; set; } = 0;

    public int? ClientId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}