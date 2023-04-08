using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductContracts;

namespace DiscountManagement.Application.Contracts.CustomerDiscountContracts
{
    public class DefineCustomerDiscount
    {
        [DisplayName("محصول")]
        [Range(1, long.MaxValue)]
        public long FkProductId { get;  set; }
        [DisplayName("علت تخفیف")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(500, ErrorMessage = ValidatingMessage.MaxLength)]
        public string Reason { get; set; }
        [DisplayName("درصد تخفیف")]
        [Range(1, int.MaxValue)]
        public int DiscountRate { get; set; }
        [DisplayName("تاریخ شروع")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(20, ErrorMessage = ValidatingMessage.MaxLength)]
        public string StartDate { get; set; }
        [DisplayName("تاریخ پایان")]
        [Required(ErrorMessage = ValidatingMessage.IsRequired)]
        [StringLength(20, ErrorMessage = ValidatingMessage.MaxLength)]
        public string EndDate { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}