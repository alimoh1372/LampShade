namespace DiscountManagement.Application.Contracts.CustomerDiscountContracts
{
    public class CustomerDiscountSearchModel
    {
        public string Reason { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long? FkProductId { get; set; }
        

    }
}