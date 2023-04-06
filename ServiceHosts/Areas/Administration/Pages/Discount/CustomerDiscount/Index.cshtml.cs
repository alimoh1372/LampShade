using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscountContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductContracts;

namespace ServiceHosts.Areas.Administration.Pages.Discount.CustomerDiscount
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<CustomerDiscountViewModel> CustomerDiscountViewModels { get; set; }

        public CustomerDiscountSearchModel SearchModel { get; set; }
        public SelectList ProductItems { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;

        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            _productApplication = productApplication;
            _customerDiscountApplication = customerDiscountApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel=null)
        {
            var product = _productApplication.Search();
            ProductItems = new SelectList(product, "Id", "Name");
            CustomerDiscountViewModels = _customerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {

            DefineCustomerDiscount model = new DefineCustomerDiscount
            {
                Products = _productApplication.Search()
            };


            return Partial("Create", model);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            EditCustomerDiscount editCustomerDiscount = _customerDiscountApplication.GetDetails(id);
            editCustomerDiscount.Products = _productApplication.Search();
            return Partial("Edit", editCustomerDiscount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            OperationResult result = _customerDiscountApplication.Edit(command);

            return new JsonResult(result);
        }
        
    }
}
