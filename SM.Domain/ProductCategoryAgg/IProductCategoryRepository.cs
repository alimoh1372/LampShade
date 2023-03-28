using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductCategoryContracts;

namespace SM.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository:IBaseRepository<long,ProductCategory>
    {
        List<ProductCategoryViewModel> Search(SearchProductCategoryModel searchModel);
        EditProductCategory GetDetails(long id);
    }
}