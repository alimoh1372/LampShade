using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductCategoryContracts
{
    public class EditProductCategory:CreateProductCategory
    {
        [Key]
        
        public long Id { get; set; }
    }
}