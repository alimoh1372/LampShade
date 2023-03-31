namespace ShopManagement.Application.Contracts.ProductContracts
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public bool IsInStock { get; set; }
        public string Code { get; set; }
        public double UnitPrice { get; set; }
        public string CreationDate { get; set; }

        public long FkProductCategoryId { get; set; }

        public string ProductCategory{ get; set; }
        
    }
}