namespace ANIMALITOS_PHARMA_API.Contracts;
public class User
{
    public int Id { get; set; } = 0;

    public string Username { get; set; } = "";

    public string Password { get; set; } = "";

    public int? EmployeeId { get; set; } = 0;

    public int StatusId { get; set; } = 0;
}

public class UserFilter()
{
    public int Id { get; set; } = 0;

    public string Username { get; set; } = "";

    public string Password { get; set; } = "";

    public int? EmployeeId { get; set; } = 0;

    public int StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}