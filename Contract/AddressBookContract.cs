namespace ANIMALITOS_PHARMA_API.Contracts;
public class AddressBook
{
    public int Id { get; set; } = 0;

    public string? Direction { get; set; } = "";

    public string Phone { get; set; } = "";

    public string? Email { get; set; } = "";

    public string? Rfc { get; set; } = "";

    public int StatusId { get; set; } = 0;
}

public class AddressBookFilter
{
    public int Id { get; set; } = 0;

    public string? Direction { get; set; } = "";

    public string Phone { get; set; } = "";

    public string? Email { get; set; } = "";

    public string? Rfc { get; set; } = "";

    public int StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}