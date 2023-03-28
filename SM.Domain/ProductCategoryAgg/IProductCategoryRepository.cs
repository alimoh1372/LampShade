using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ShopManagement.Application.Contracts.ProductCategoryContracts;

namespace SM.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity);
        ProductCategory Get(long id);
        List<ProductCategory> GetAll();
        bool IsExists(Expression<Func<ProductCategory, bool>> predicate);
        void SaveChanges();
        List<ProductCategoryViewModel> Search(SearchProductCategoryModel searchModel);
    }
}