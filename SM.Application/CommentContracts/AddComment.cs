using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.CommentContracts
{
    public class AddComment
    {
        [DisplayName("نام")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(255, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Name { get; set; }
        [DisplayName("نام")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(255, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Email { get; set; }
        [DisplayName("متن پیام")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(2000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Message { get; set; }
        [DisplayName("امتیاز")]
        [Range(1,5,ErrorMessage = ValidatingMessage.NumberOutOfRange)]
        public int Point { get;  set; }

        public long FkProductId { get;  set; }
        
    }
}