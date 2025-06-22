public class Vendor
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "";

    public int? AddressId { get; set; } = 0;

    public int StatusId { get; set; } = 0;
}

public class VendorFilter
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "";

    public int? AddressId { get; set; } = 0;

    public int StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}