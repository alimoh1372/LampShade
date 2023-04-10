namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public class CreateInventory
    {
        public long FkProductId { get;  set; }
        public double UnitPrice { get;  set; }
        public string Product { get; set; }

    }
}