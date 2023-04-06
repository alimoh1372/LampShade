using System;
using _0_Framework.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount:EntityBase
    {
        public long FkProductId { get; private set; }
        public string Reason { get; private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public CustomerDiscount(long fkProductId, string reason, DateTime startDate, DateTime endDate, int discountRate)
        {
            FkProductId = fkProductId;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
            DiscountRate = discountRate;
        }
        public void Edit(long fkProductId, string reason, DateTime startDate, DateTime endDate, int discountRate)
        {
            FkProductId = fkProductId;
            Reason = reason;
            StartDate = startDate;
            EndDate = endDate;
            DiscountRate = discountRate;
        }


    }
}