using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.ProductCategoryContracts
{
    public class EditProductCategory:CreateProductCategory
    {
        public long Id { get; set; }
        
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidatingMessage.MaxFileSize)]
        [FileExtension(new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidatingMessage.FileExtension)]
        public new IFormFile Picture { get; set; }

        public string PicturePath { get; set; }
    }
}