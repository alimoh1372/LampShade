using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.SlideContracts
{
    public class CreateSlide        
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

    }
}