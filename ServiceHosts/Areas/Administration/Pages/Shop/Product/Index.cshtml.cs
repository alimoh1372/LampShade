using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Application.Contracts.ProductContracts;

namespace ServiceHosts.Areas.Administration.Pages.Shop.Product
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<ProductViewModel> ProductViewModels { get; set; }

        public ProductSearchModel SearchModel { get; set; }
        public List<SelectListItem>  ProductCategoryItems { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            ProductCategoryItems = _productCategoryApplication.Search()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();
            ProductViewModels = _productApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            CreateProduct model = new CreateProduct
            {
                Name = null,
                Code = null,
                UnitPrice = 0,
                ShortDescription = null,
                Description = null,
                Picture = null,
                PictureAlt = null,
                PictureTitle = null,
                Slug = null,
                Keywords = null,
                MetaDescription = null,
                FkCategoryId = 0,
                ProductCategories = _productCategoryApplication.Search(null)
        };
            
            return Partial("Create", model);
        }

        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            EditProduct editProduct = _productApplication.GetDetails(id);
            editProduct.ProductCategories = _productCategoryApplication.Search();
            return Partial("Edit", editProduct);
        }

        public JsonResult OnPostEdit(EditProduct command)
        {
            OperationResult result = _productApplication.Edit(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetExistInStock(long id)
        {
            OperationResult result = _productApplication.ExistsInStock(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                Message = result.Message;
                return RedirectToPage("./Index");

            }
        }
        public IActionResult OnGetRunningOutInStock(long id)
        {
            OperationResult result = _productApplication.RunningOutInStock(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                Message = result.Message;
                return RedirectToPage("./Index");

            }
        }
    }
}
