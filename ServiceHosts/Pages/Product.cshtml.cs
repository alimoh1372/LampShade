using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.CommentContracts;

namespace ServiceHosts.Pages
{
    public class ProductModel : PageModel
    {
        [TempData] public string Message { get; set; }
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductQueryModel Product { get; set; }
        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetProductDetailsBy(id);
        }

        public RedirectToPageResult OnPost(AddComment command, string slug)
        {
            OperationResult result = _commentApplication.Add(command);
            if (!result.IsSuccedded)
            {
                Message = result.Message;
            }

            return RedirectToPage("/Product", new { id = slug });
        }
    }
}
