namespace InventoryManagement.ApplicationContracts.InventoryContracts
{
    public class InventoryOperationsViewModel
    {
        public long Id { get;  set; }

        public bool Operation { get;  set; }
        public long Count { get;  set; }
        public long FkOperatorId { get;  set; }
        public string Operator { get; set;  }
        public string OperationDate { get;  set; }
        public long CurrentCount { get;  set; }
        public string Description { get;  set; }
        public long OrderId { get;  set; }
    }
}