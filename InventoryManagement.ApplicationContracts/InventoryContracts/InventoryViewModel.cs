namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public class InventoryViewModel
    {
        public long Id { get; set; }
        public double UnitPrice { get; set; }
        public string Product { get; set; }
        public bool IsInStock { get; set; }
        public long CurrentCount { get; set; }

    }
}