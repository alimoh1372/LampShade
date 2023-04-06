using _0_Framework.Domain;
using ShopManagement.Application.Contracts.SlideContracts;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public class ColleagueDiscount:EntityBase
    {
        public long FkProductId { get; private set; }
        public bool IsActive { get; private set; }
        public int DiscountRate { get; private set; }

        public ColleagueDiscount(long fkProductId, int discountRate)
        {
            FkProductId = fkProductId;
            DiscountRate = discountRate;
            IsActive = true;
        }
        public void Edit(long fkProductId, int discountRate)
        {
            FkProductId = fkProductId;
            DiscountRate = discountRate;
        }

        public void Active()
        {
            IsActive = true;
        }

        public void Remove()
        {
            IsActive = false;
        }
    }
}