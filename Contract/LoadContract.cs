public class Load
{
    public int Id { get; set; } = 0;

    public DateTime? CreatedDate { get; set; } = DateTime.MinValue;

    public int? EmployeeId { get; set; } = 0;

    public double? LoadValue { get; set; } = 0;

    public int? StatusId { get; set; } = 0;
}
public class LoadFilter
{
    public int Id { get; set; } = 0;

    public DateTime? CreatedDate { get; set; } = DateTime.MinValue;

    public int? EmployeeId { get; set; } = 0;

    public double? LoadValue { get; set; } = 0;

    public int? StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}