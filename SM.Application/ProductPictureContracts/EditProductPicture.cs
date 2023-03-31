using ShopManagement.Application.Contracts.ProductContracts;

namespace ShopManagement.Application.Contracts.ProductPictureContracts
{
    public class EditProductPicture : CreateProductPicture
    {
        public long Id { get; set; }
    }
}