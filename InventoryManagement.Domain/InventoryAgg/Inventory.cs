using System.Collections.Generic;
using System.Linq;
using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory:EntityBase
    {
        public long FkProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public List<InventoryOperation> InventoryOperations { get; private set; }

        public Inventory(long fkProductId, double unitPrice)
        {
            FkProductId = fkProductId;
            UnitPrice = unitPrice;
            IsInStock = false;
        }
        public void Edit(long fkProductId, double unitPrice)
        {
            FkProductId = fkProductId;
            UnitPrice = unitPrice;
        }

        public long CalculateCurrentCount()
        {
            var plus = InventoryOperations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = InventoryOperations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(long count, long fkOperatorId, string description)
        {
            long currentCount = CalculateCurrentCount() + count;
            InventoryOperation inventoryOperation =
                new InventoryOperation(true, count, fkOperatorId, currentCount, description, 0, Id);
            
            InventoryOperations.Add(inventoryOperation);
            IsInStock = CalculateCurrentCount() > 0;
        }

        public void Reduce(long count, long fkOperatorId, long fkOrderId, string description)
        {
            long currantCount = CalculateCurrentCount() - count;
            InventoryOperation inventoryOperation =
                new InventoryOperation(false, count, fkOperatorId, currantCount, description, fkOrderId, Id);
            InventoryOperations.Add(inventoryOperation);
            IsInStock = CalculateCurrentCount() > 0;
        }
    }
}