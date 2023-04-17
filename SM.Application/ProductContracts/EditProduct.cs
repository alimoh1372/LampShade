using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.ProductContracts
{
    public class EditProduct:CreateProduct
    {
        public long Id { get; set; }
        public string PicturePath { get; set; }
        [DisplayName("تصویر محصول")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidatingMessage.MaxLength)]
        [FileExtension(new[] { ".jpeg", ".jpg", ".png" })]
        public new IFormFile Picture { get; set; }
    }
}