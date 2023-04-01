using System.Globalization;

namespace ShopManagement.Application.Contracts.ProductPictureContracts
{
    public class ProductPictureViewModel
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public  string CreationDate { get; set; }
        public string Product { get; set; }
        public long FkProductId { get; set; }
        public bool IsRemoved { get; set; }
    }
}