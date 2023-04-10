namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public class IncreaseInventory
    {
        public long Count { get; set; }
        public long FkOperatorId { get; set; }
        public string Description { get; set; }
    }
}