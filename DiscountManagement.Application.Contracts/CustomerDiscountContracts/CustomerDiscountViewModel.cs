namespace DiscountManagement.Application.Contracts.CustomerDiscountContracts
{
    public class CustomerDiscountViewModel
    {
        public long Id { get; set; }
        public string Reason { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long FkProductId { get; set; }
        public string Product { get; set; }
        public int DiscountRate { get; set; }
    }
}