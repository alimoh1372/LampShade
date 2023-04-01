using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductContracts
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);

        OperationResult Edit(EditProduct command);

        EditProduct GetDetails(long id);
        OperationResult ExistsInStock(long id);
        OperationResult RunningOutInStock(long id);

        List<ProductViewModel> Search(ProductSearchModel searchModel=null);

        
    }
}