using System.Collections.Generic;
using SM.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.SM.Application.Contracts.ProductCategoryContracts
{
    public interface IProductCategoryApplication
    {
        void Create(CreateProductCategory command);
        void Edit(EditProductCategory command);
        ProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(SearchProductCategoryModel searchModel);
    }
}