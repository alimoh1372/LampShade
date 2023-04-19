namespace _01_LampshadeQuery.Contracts.ProductPicture
{
    public class ProductPictureQueryModel
    {
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public bool IsRemoved { get;  set; }

        public long FkProductId { get;  set; }
    }
}