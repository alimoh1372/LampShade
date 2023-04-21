using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.ArticleCategoryContracts
{
    public class CreateArticleCategory
    {

        [DisplayName("نام")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(255, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Name { get;  set; }
        [DisplayName("تصویر")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidatingMessage.MaxLength)]
        [FileExtension(new[] { ".jpeg", ".jpg", ".png" })]
        public IFormFile Picture { get;  set; }

        [DisplayName("Alt تصویر(SEO)")]
        [StringLength(300, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureAlt { get; set; }

        [DisplayName("Title تصویر(SEO)")]
        [StringLength(400, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureTitle { get; set; }

        [DisplayName("توضیحات")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(2000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Description { get;  set; }
        [DisplayName("ترتیب نمایش")]
        public int ShowOrder { get;  set; }

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
        [DisplayName("آدرس متعارف SEO")]
        [StringLength(1000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string CanonicalAddress { get;  set; }
    }
}