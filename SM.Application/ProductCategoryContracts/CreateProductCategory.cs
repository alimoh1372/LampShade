using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ShopManagement.Application.Contracts.ProductCategoryContracts
{
    public class CreateProductCategory
    {
        [DisplayName("نام")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(255,ErrorMessage = ValidatingMessage.MaxLength)]
        public string Name { get;  set; }

        [DisplayName("توضیحات")]
        [StringLength(500, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Description { get;  set; }
       
        public IFormFile Picture { get;  set; }
        [DisplayName("Alt تصویر")]
        [StringLength(255, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureAlt { get;  set; }

        [DisplayName("Title تصویر")]
        [StringLength(500, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureTitle { get;  set; }

        [DisplayName("کلمات کلیدی")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(80, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Keywords { get;  set; }

        [DisplayName("توضیحات SEO")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(150, ErrorMessage = ValidatingMessage.MaxLength)]
        
        public string MetaDescription { get;  set; }

        [DisplayName("اسلاگ")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(255, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Slug { get;  set; }
    }
}