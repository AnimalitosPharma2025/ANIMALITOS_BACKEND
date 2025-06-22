
public class Client
{
    public int Id { get; set; } = 0;
    public string Name { get; set; } = "";
    public double? CreditLimit { get; set; } = 0;
    public int? AddressId { get; set; } = 0;
    public int StatusId { get; set; } = 0;
}

public class ClientFilter
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "";

    public double? CreditLimit { get; set; } = 0;

    public int? AddressId { get; set; } = 0;

    public int StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}
