using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Application.Contracts.ProductContracts;
using ShopManagement.Application.Contracts.ProductPictureContracts;
using ShopManagement.Application.Contracts.SlideContracts;

namespace ServiceHosts.Areas.Administration.Pages.Shop.ProductPicture
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<ProductPictureViewModel> ProductPictureViewModels { get; set; }

        public ProductPictureSearchModel SearchModel { get; set; }
        public List<SelectListItem>  ProductItems { get; set; }
        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            ProductItems = _productApplication.Search()
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList();
            ProductPictureViewModels = _productPictureApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            CreateProductPicture model = new CreateProductPicture
            {
               
                Picture = null,
                PictureAlt = null,
                PictureTitle = null,
                
                Products = _productApplication.Search(null)
        };
            
            return Partial("Create", model);
        }

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            EditProductPicture editProductPicture = _productPictureApplication.GetDetails(id);
            editProductPicture.Products = _productApplication.Search();
            return Partial("Edit", editProductPicture);
        }

        public JsonResult OnPostEdit(EditProductPicture command)
        {
            OperationResult result = _productPictureApplication.Edit(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            OperationResult result = _productPictureApplication.Remove(id);
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
        public IActionResult OnGetActive(long id)
        {
            OperationResult result = _productPictureApplication.Active(id);
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
