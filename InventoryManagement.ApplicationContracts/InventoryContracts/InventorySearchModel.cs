using System.ComponentModel;

namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public class InventorySearchModel   
    {
        public double? UnitPrice { get; set; }
        public long FkProductId { get; set; }
        [DisplayName("موجودها")]
        public bool IsInStock { get; set; }
        [DisplayName("ناموجودها")]
        public  bool NotInStock { get; set; }
    }
}