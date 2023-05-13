using System.Collections.Generic;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using Microsoft.AspNetCore.Authorization;

namespace ServiceHosts.Areas.Administration.Pages.Shop.ProductCategorie
{
    //[Authorize(Roles = "1, 3")]
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategoryViewModels { get; set; }
       
        public SearchProductCategoryModel SearchModel { get; set; }
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
           
        }

        public void OnGet(SearchProductCategoryModel searchModel)
        {
            ProductCategoryViewModels = _productCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("RegisterAccount", new CreateProductCategory());
        }

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            EditProductCategory editProduct = _productCategoryApplication.GetDetails(id);
            return Partial("Edit", editProduct);
        }

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
