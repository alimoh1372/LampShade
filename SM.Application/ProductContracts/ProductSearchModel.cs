using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ShopManagement.Application.Contracts.ProductContracts
{
    public class ProductSearchModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
      
        public double? UnitPrice { get; set; }
        public long FkCategoryId { get; set; }
    }
}