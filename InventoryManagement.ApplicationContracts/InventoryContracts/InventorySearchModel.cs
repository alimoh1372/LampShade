namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public class InventorySearchModel   
    {
        public double? UnitPrice { get; set; }
        public long FkProductId { get; set; }
        public bool IsInStock { get; set; }
        public  bool NotInStock { get; set; }
    }
}