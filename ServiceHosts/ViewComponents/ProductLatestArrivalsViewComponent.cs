using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHosts.ViewComponents
{
    public class ProductLatestArrivalsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public ProductLatestArrivalsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public IViewComponentResult Invoke()
        {
            var latestProductsQueryModels = _productQuery.GetLatestArrivals(6);
            return View(latestProductsQueryModels);
        }
    }
}