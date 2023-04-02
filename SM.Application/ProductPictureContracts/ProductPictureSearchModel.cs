namespace ShopManagement.Application.Contracts.SlideContracts
{
    public class ProductPictureSearchModel
    {
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public long FkProductId { get; set; }

    }
}