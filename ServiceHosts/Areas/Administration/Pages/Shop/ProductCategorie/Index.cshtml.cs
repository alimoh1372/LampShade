using System.Collections.Generic;
using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryContracts;

namespace ServiceHosts.Areas.Administration.Pages.Shop.ProductCategorie
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategoryViewModels { get; set; }
        private readonly IFileUpload _fileUpload;
        private readonly IWebHostBuilder _webHostBuilder;
        public SearchProductCategoryModel SearchModel { get; set; }
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication, IFileUpload fileUpload)
        {
            _productCategoryApplication = productCategoryApplication;
            _fileUpload = fileUpload;
        }

        public void OnGet(SearchProductCategoryModel searchModel)
        {
            ProductCategoryViewModels = _productCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("Create", new CreateProductCategory());
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
