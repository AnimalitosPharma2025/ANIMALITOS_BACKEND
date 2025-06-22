public class Employee
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Rol { get; set; } = null!;

    public int? AddressId { get; set; } = 0;

    public int StatusId { get; set; } = 0;
}

public class EmployeeFilter
{
    public int Id { get; set; } = 0;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Rol { get; set; } = null!;

    public int? AddressId { get; set; } = 0;

    public int StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}