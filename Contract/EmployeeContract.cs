public class Employee
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Rol { get; set; } = "";

    public int? AddressId { get; set; } = 0;

    public int StatusId { get; set; } = 0;

    public double? DailyAmount { get; set; } = 0;
}

public class EmployeeFilter
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? Rol { get; set; } = "";

    public int? AddressId { get; set; } = 0;

    public int StatusId { get; set; } = 0;

    public double? DailyAmount { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}