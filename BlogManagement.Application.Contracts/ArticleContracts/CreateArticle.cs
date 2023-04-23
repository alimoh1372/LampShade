using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.ArticleContracts
{
    public class CreateArticle
    {
        [DisplayName("عنوان")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(255, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Title { get;  set; }
        [DisplayName("توضیحات کوتاه")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(1000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string ShortDescription { get;  set; }

        [DisplayName("توضیحات")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        public string Description { get;  set; }

        [DisplayName("تصویر")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidatingMessage.MaxLength)]
        [FileExtension(new[] { ".jpeg", ".jpg", ".png" },ErrorMessage = ValidatingMessage.FileExtension)]
        public IFormFile Picture { get; set; }


        [DisplayName("Alt تصویر(SEO)")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(300, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureAlt { get; set; }

        [DisplayName("Title تصویر(SEO)")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(400, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureTitle { get; set; }
        [DisplayName("تاریخ انتشار")]
        public string PublishDate { get;  set; }

        [DisplayName("اسلاگ(SEO)")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(300, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Slug { get; set; }


        [DisplayName("کلمات کلیدی")]
        [StringLength(80, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Keywords { get; set; }
        [DisplayName("توضیحات SEO")]
        [StringLength(150, ErrorMessage = ValidatingMessage.MaxLength)]
        public string MetaDescription { get; set; }
        [DisplayName("آدرس متعارف SEO")]
        [StringLength(1000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string CanonicalAddress { get; set; }

        [DisplayName("گروه مقاله")]
        [Range(1,long.MaxValue,ErrorMessage = ValidatingMessage.IsRequired)]
        public long FkArticleCategoryId { get;  set; }

    }
}