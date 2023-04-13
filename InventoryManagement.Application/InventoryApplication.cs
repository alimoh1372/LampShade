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

        public OperationResult Create(CreateInventory command)
        {
            OperationResult result = new OperationResult();
            if (_inventoryRepository.IsExists(x => x.FkProductId == command.FkProductId))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            Inventory inventory = new Inventory(command.FkProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Edit(EditInventory command)
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

        public OperationResult Increase(IncreaseInventory command)
        {
            OperationResult result = new OperationResult();
            Inventory inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory ==null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }

            const long fkOperatorId = 1;
            inventory.Increase(command.Count,fkOperatorId,command.Description);
            _inventoryRepository.SaveChanges();
            return result.Succedded();

        }

        public OperationResult ReduceInventories(List<ReduceInventory> command)
        {
            OperationResult result = new OperationResult();
            const long fkOperatorId = 1;
            foreach (var item in command)
            {
                Inventory inventory = _inventoryRepository.Get(item.InventoryId);
                if (inventory == null)
                {
                    return result.Failed(ApplicationMessage.NotFound);
                }
                inventory.Reduce(item.Count, fkOperatorId,item.FkOrderId, item.Description);
            }
           
            _inventoryRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            OperationResult result = new OperationResult();
            Inventory inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }

            const long fkOperatorId = 1;
            inventory.Reduce(command.Count, fkOperatorId,0, command.Description);
            _inventoryRepository.SaveChanges();
            return result.Succedded();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }

        public List<InventoryOperationsViewModel> GetInventoryOperations(long inventoryId)
        {
            return _inventoryRepository.GetInventoryOperations(inventoryId);
        }
    }
}
