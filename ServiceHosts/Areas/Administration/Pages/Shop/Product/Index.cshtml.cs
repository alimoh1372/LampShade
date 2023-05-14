using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Application.Contracts.ProductContracts;
using ShopManagement.Configuration.Permissions;

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
        [NeedsPermission(ShopPermissions.ListProducts)]
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
        [NeedsPermission(ShopPermissions.ListProducts)]
        public IActionResult OnGetCreate()
        {
            CreateProduct model = new CreateProduct
            {
                Name = null,
                Code = null,
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
            
            return Partial("RegisterAccount", model);
        }
        [NeedsPermission(ShopPermissions.ListProducts)]
        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }
        [NeedsPermission(ShopPermissions.CreateProduct)]
        public IActionResult OnGetEdit(long id)
        {
            EditProduct editProduct = _productApplication.GetDetails(id);
            editProduct.ProductCategories = _productCategoryApplication.Search();
            return Partial("Edit", editProduct);
        }
        [NeedsPermission(ShopPermissions.EditProduct)]
        public JsonResult OnPostEdit(EditProduct command)
        {
            OperationResult result = _productApplication.Edit(command);

            return new JsonResult(result);
        }

       
       
    }
}
