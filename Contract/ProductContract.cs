public class Product
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "";

    public string? Description { get; set; } = "";

    public string Category { get; set; } = "";

    public double PurchasePrice { get; set; } = 0;

    public double UnitPrice { get; set; } = 0;

    public string Code { get; set; } = "";

    public string? ImageUrl { get; set; } = "";

    public double? Discount { get; set; } = 0;

    public int VendorId { get; set; } = 0;

    public int StatusId { get; set; } = 0;
}

public class ProductFilter
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "";

    public string? Description { get; set; } = "";

    public string Category { get; set; } = "";

    public double PurchasePrice { get; set; } = 0;

    public double UnitPrice { get; set; } = 0;

    public int VendorId { get; set; } = 0;

    public int StatusId { get; set; } = 0;

    public string Code { get; set; } = "";

    public string? ImageUrl { get; set; } = "";

    public double? Discount { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}