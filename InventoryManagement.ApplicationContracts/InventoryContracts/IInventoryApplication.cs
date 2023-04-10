using System.Collections.Generic;
using _0_Framework.Application;

namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public interface IInventoryApplication
    {
        OperationResult CreateInventory(CreateInventory command);
        OperationResult EditInventory(EditInventory command);
        OperationResult IncreaseInventory(IncreaseInventory command);
        OperationResult ReduceInventories(List<ReduceInventory> command);
        OperationResult ReduceInventory(ReduceInventory command);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);

    }
}