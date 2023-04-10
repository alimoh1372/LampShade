namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public class ReduceInventory
    {
        public long InventoryId { get; set; }
        public long FkProductId { get; set; }
        public long Count { get; set; }
        public long FkOrderId { get; set; }
        public string Description { get; set; }
    }
}