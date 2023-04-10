namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public class ReduceInventory
    {
        public long Count { get; set; }
        public long FkOperatorId { get; set; }
        public long FkOrderId { get; set; }
        public string Description { get; set; }
    }
}