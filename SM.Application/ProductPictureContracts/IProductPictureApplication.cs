using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.SlideContracts
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);
        OperationResult Edit(EditProductPicture command);

        OperationResult Remove(long id);
        OperationResult Active(long id);

        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel = null);
    }
}