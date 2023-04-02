using ShopManagement.Application.Contracts.ProductContracts;

namespace ShopManagement.Application.Contracts.SlideContracts
{
    public class EditProductPicture : CreateProductPicture
    {
        public long Id { get; set; }
    }
}