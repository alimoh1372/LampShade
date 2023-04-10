using System.Collections.Generic;
using _0_Framework.Domain;
using InventoryManagement.ApplicationContracts.InventoryContracts;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository:IBaseRepository<long,Inventory>
    {
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        Inventory GetBy(long productId);
    }
}