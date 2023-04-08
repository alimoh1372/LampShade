using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ShopManagement.Application.Contracts.ProductContracts;

namespace DiscountManagement.Application.Contracts.ColleagueDiscountContracts
{
    public class DefineColleagueDiscount
    {

        [DisplayName("محصول")]
        [Range(1, long.MaxValue)]
        public long FkProductId { get; set; }


        [DisplayName("درصد تخفیف")]
        [Range(1, int.MaxValue)]
        public int DiscountRate { get; set; }

     

        public List<ProductViewModel> Products { get; set; }
    }
}