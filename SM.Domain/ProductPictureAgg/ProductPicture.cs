using _0_Framework.Domain;
using SM.Domain.ProductAgg;

namespace SM.Domain.ProductPictureAgg
{
    public class ProductPicture:EntityBase
    {
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public bool IsRemoved { get; private set; }

        public long FkProductId { get;private set; }
        public Product Product { get; private set; }

        public ProductPicture(string picture, string pictureAlt, string pictureTitle, long fkProductId)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            FkProductId = fkProductId;
            IsRemoved = false;
        }
        public void Edit(string picture, string pictureAlt, string pictureTitle, long fkProductId)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            FkProductId = fkProductId;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Active()
        {
            IsRemoved = false;
        }
    }
}