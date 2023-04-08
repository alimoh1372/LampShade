using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscountContracts;
using DiscountManagement.Application.Contracts.CustomerDiscountContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductContracts;

namespace ServiceHosts.Areas.Administration.Pages.Discount.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscountViewModels { get; set; }

        public ColleagueDiscountSearchModel SearchModel { get; set; }
        public SelectList ProductItems { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel=null)
        {
            var product = _productApplication.Search();
            ProductItems = new SelectList(product, "Id", "Name");
            ColleagueDiscountViewModels = _colleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {

            DefineColleagueDiscount model = new DefineColleagueDiscount
            {
                Products = _productApplication.Search()
            };


            return Partial("Create", model);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            EditColleagueDiscount editCustomerDiscount = _colleagueDiscountApplication.GetDetails(id);
            editCustomerDiscount.Products = _productApplication.Search();
            return Partial("Edit", editCustomerDiscount);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            OperationResult result = _colleagueDiscountApplication.Edit(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetActive(long id)
        {
            OperationResult result = _colleagueDiscountApplication.Active(id);
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
        public IActionResult OnGetRemove(long id)
        {
            OperationResult result = _colleagueDiscountApplication.Remove(id);
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
