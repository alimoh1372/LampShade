using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class SearchModel : PageModel
    {
        public List<ProductQueryModel> ProductQueries { get; set; }
        public string Value { get; set; }
        private readonly IProductQuery _productQuery;

        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string value)
        {
            Value = value;
            ProductQueries = _productQuery.GetProductQueryModelsBy(value);
        }
    }
}
