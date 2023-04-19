using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        public ProductQueryModel Product { get; set; }
        public ProductModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetProductDetailsBy(id);
        }
    }
}
