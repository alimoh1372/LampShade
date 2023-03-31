using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductContracts;

namespace ShopManagement.Application.Contracts.ProductPictureContracts
{
    public class CreateProductPicture
    {
        [DisplayName("تصویر محصول")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(1000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Picture { get;  set; }

        [DisplayName("Alt تصویر(SEO)")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(300, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureAlt { get;  set; }

        [DisplayName("Title تصویر(SEO)")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(400, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureTitle { get;  set; }

        [DisplayName("محصول")]
        [Range(1,long.MaxValue)]
        public long FkProductId { get;  set; }

        public List<ProductViewModel> Products { get; set; }

    }
}