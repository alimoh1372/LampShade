using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _01_LampshadeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
       
        List<ProductCategoryQueryModel> GetProductCategoryQueryModel();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProductsQueryModel();
        ProductCategoryQueryModel GetProductCategoryQueryModelBy(string slug);
        
    }
}