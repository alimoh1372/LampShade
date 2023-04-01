using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductContracts;

namespace SM.Domain.ProductAgg
{
    public interface IProductRepository:IBaseRepository<long,Product>
    {
        EditProduct GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel=null);

    }
}