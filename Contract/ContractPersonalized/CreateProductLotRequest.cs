namespace ANIMALITOS_PHARMA_API.Contract.ContractPersonalized
{
    public class CreateProductLotRequest
    {
        public required ProductLot ProductLot { get; set; }
        public required InventoryItem InventoryItem { get; set; }
        public int QuantityItems { get; set; }
    }

}
