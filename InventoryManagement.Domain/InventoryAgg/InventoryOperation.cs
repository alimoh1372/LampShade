using System;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class InventoryOperation
    {
        public long Id { get; private set; }

        public bool Operation { get; private set; }
        public long Count { get; private set; }
        public long FkOperatorId { get; private set; }
        public DateTime OperationDate { get; private set; }
        public long CurrentCount { get; private set; }
        public string Description { get; private set; }
        public long OrderId { get; private set; }
        public long FkInventoryId { get; private set; }

        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool operation, long count, long fkOperatorId, long currentCount,
            string description, long orderId, long fkInventoryId)
        {
            Operation = operation;
            Count = count;
            FkOperatorId = fkOperatorId;
            CurrentCount = currentCount;
            Description = description;
            OrderId = orderId;
            FkInventoryId = fkInventoryId;
            OperationDate=DateTime.Now;

        }
    }
}