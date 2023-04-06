namespace DiscountManagement.Application.Contracts.ColleagueDiscountContracts
{
    public class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public long FkProductId { get; set; }
        public string Product { get; set; }
        public string CreationDate { get; set; }
        public int DiscountRate { get; set; }
        public bool IsActive { get; set; }
    }
}