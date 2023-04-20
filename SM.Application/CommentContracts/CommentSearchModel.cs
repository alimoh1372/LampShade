using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.CommentContracts
{
    public class CommentSearchModel
    {
        [DisplayName("نام")]
        [StringLength(255, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Name { get; set; }
       
        public string Email { get; set; }
        [DisplayName("متن پیام")]
        [StringLength(2000, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Message { get; set; }

        public long? FkProductId { get; set; }

    }
}