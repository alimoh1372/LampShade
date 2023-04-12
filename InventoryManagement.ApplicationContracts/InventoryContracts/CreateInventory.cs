using ShopManagement.Application.Contracts.ProductContracts;
using System.Collections.Generic;

namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public class CreateInventory
    {
        public long FkProductId { get;  set; }
        public double UnitPrice { get;  set; }
        public List<ProductViewModel> Products { get; set; }
    }
}