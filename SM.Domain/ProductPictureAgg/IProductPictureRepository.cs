using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPictureContracts;
using ShopManagement.Application.Contracts.SlideContracts;

namespace SM.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository:IBaseRepository<long,ProductPicture>
    {
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel = null);
    }
}