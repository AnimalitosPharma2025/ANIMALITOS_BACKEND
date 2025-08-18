
namespace ANIMALITOS_PHARMA_API.Contracts;

public class LoadsContent
{
    public int Id { get; set; } = 0;

    public int? LoadId { get; set; } = 0;

    public int? InventoryId { get; set; } = 0;

    public int? StatusId { get; set; } = 0;
}

public class LoadsContentFilter
{
    public int Id { get; set; } = 0;

    public int? LoadId { get; set; } = 0;

    public int? InventoryId { get; set; } = 0;

    public int? StatusId { get; set; } = 0;

    public string SortColumn { get; set; } = "";

    public int PagingBegin { get; set; } = -1;

    public int PagingRange { get; set; } = -1;
}
