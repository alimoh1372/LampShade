using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using Microsoft.AspNetCore.Authorization;
using ShopManagement.Configuration.Permissions;

namespace ServiceHosts.Areas.Administration.Pages.Shop.ProductCategorie
{
    
    [Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategoryViewModels { get; set; }
       
        public SearchProductCategoryModel SearchModel { get; set; }
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
           
        }
        [NeedsPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(SearchProductCategoryModel searchModel)
        {
            ProductCategoryViewModels = _productCategoryApplication.Search(searchModel);
        }
        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public IActionResult OnGetCreate()
        {
            return Partial("RegisterAccount", new CreateProductCategory());
        }
        [NeedsPermission(ShopPermissions.CreateProductCategory)]
        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }
        [NeedsPermission(ShopPermissions.EditProductCategory)]
        public IActionResult OnGetEdit(long id)
        {
            EditProductCategory editProduct = _productCategoryApplication.GetDetails(id);
            return Partial("Edit", editProduct);
        }
        [NeedsPermission(ShopPermissions.EditProductCategory)]
        public JsonResult OnPostEdit(EditProductCategory command)
        {
            bool isValid = ModelState.IsValid;
            if (isValid)
            {
                
            }
            OperationResult result = _productCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
