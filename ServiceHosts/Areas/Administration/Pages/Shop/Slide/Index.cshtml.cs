using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.SlideContracts;

namespace ServiceHosts.Areas.Administration.Pages.Shop.Slide
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<SlideViewModel> SlideViewModels { get; set; }

        public SlideSearchModel SearchModel { get; set; }
        public List<SelectListItem> ProductItems { get; set; }
        private readonly ISlideApplication _slideApplication;


        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;

        }

        public void OnGet(SlideSearchModel searchModel)
        {
            SlideViewModels = _slideApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("Create");
        }

        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = _slideApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            EditSlide editSlide = _slideApplication.GetDetails(id);
            return Partial("Edit", editSlide);
        }

        public JsonResult OnPostEdit(EditSlide command)
        {
            OperationResult result = _slideApplication.Edit(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetDeActive(long id)
        {
            OperationResult result = _slideApplication.DeActive(id);
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
            OperationResult result = _slideApplication.Active(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
