using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategoryContracts;

namespace ShopManagement.Application.Contracts.ProductContracts
{
    public class CreateProduct
    {
        [DisplayName("نام")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(255, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Name { get;  set; }
        [DisplayName("کد محصول")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(15, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Code { get;  set; }

        [DisplayName("قیمت واحد")]
        [Range(1, 100000, ErrorMessage = ValidatingMessage.IsRequired)]
       
        public double UnitPrice { get;  set; }



        [DisplayName("توضیحات کوتاه")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(500, ErrorMessage = ValidatingMessage.MaxLength)]
        public string ShortDescription { get;  set; }


        [DisplayName("توضیحات")]
        [StringLength(2000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Description { get;  set; }

        [DisplayName("تصویر محصول")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(1000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Picture { get;  set; }



        [DisplayName("Alt تصویر(SEO)")]
        [StringLength(300, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureAlt { get;  set; }

        [DisplayName("Title تصویر(SEO)")]
        [StringLength(400, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureTitle { get;  set; }

        [DisplayName("اسلاگ(SEO)")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(300, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Slug { get;  set; }

        [DisplayName("کلمات کلیدی")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(80, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Keywords { get;  set; }

        [DisplayName("توضیحات SEO")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(150, ErrorMessage = ValidatingMessage.MaxLength)]
        public string MetaDescription { get;  set; }

        [DisplayName("گروه محصول")]
        public long FkCategoryId { get;  set; }
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
    }

}