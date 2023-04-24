using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHosts.ViewComponents
{
    public class MenuViewComponent: ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery productCategoryQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var menu = new MenuModel()
            {
                ProductCategories = _productCategoryQuery.GetProductCategoriesWithProductsQueryModel(),
                ArticleCategories = _articleCategoryQuery.GetArticleCategoriesForMenu()
            };
            return View(menu);
        }
    }
}