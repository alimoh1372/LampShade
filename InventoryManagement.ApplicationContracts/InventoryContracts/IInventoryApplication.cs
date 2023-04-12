using System.Collections.Generic;
using _0_Framework.Application;

namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult ReduceInventories(List<ReduceInventory> command);
        OperationResult Reduce(ReduceInventory command);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);

    }
}