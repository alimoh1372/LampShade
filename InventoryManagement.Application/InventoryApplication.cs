using System;
using System.Collections.Generic;
using _0_Framework.Application;
using InventoryManagement.ApplicationContracts.InventoryContracts;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult CreateInventory(CreateInventory command)
        {
            OperationResult result = new OperationResult();
            if (_inventoryRepository.IsExists(x => x.FkProductId == command.FkProductId))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            Inventory inventory = new Inventory(command.FkProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult EditInventory(EditInventory command)
        {
            OperationResult result = new OperationResult();
            if (_inventoryRepository.IsExists(x => x.FkProductId == command.FkProductId && x.Id != command.Id))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            Inventory inventory = _inventoryRepository.GetBy(command.FkProductId);
            if (inventory ==null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            inventory.Edit(command.FkProductId, command.UnitPrice);
           
            _inventoryRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult IncreaseInventory(IncreaseInventory command)
        {
            OperationResult result = new OperationResult();
        }

        public OperationResult ReduceInventories(List<ReduceInventory> command)
        {
            throw new NotImplementedException();
        }

        public OperationResult ReduceInventory(ReduceInventory command)
        {
            throw new NotImplementedException();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }
    }
}
