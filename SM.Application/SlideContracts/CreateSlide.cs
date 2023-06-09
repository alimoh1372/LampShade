﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.SlideContracts
{
    public class CreateSlide        
    {
        [DisplayName("تصویر محصول")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidatingMessage.MaxLength)]
        [FileExtension(new[] { ".jpeg", ".jpg", ".png" })]
        public IFormFile Picture { get; set; }
        [DisplayName("Alt تصویر(SEO)")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(300, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureAlt { get;  set; }

        [DisplayName("Title تصویر(SEO)")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(400, ErrorMessage = ValidatingMessage.MaxLength)]
        public string PictureTitle { get;  set; }
        [DisplayName("متن سر تیتر")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(255, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Heading { get;  set; }
        [DisplayName("عنوان اسلاید")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(400, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Title { get;  set; }
        [DisplayName("متن اسلاید")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(500, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Text { get;  set; }

        [DisplayName("عنوان دکمه اسلاید")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(50, ErrorMessage = ValidatingMessage.MaxLength)]
        public string BtnText { get;  set; }
        [DisplayName("لینک")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(1000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Link { get; set; }

    }
}