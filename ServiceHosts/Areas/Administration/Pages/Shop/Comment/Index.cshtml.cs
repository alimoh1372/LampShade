using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.CommentContracts;
using ShopManagement.Application.Contracts.ProductContracts;

namespace ServiceHosts.Areas.Administration.Pages.Shop.Comment
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }


        public List<CommentViewModel> Comments { get; set; }
        public CommentSearchModel SearchModel { get; set; }
        public List<SelectListItem> ProductItems { get; set; }
        private readonly ICommentApplication _commentApplication;
        private readonly IProductApplication _productApplication;
        
        public IndexModel(ICommentApplication commentApplication, IProductApplication productApplication)
        {
            _commentApplication = commentApplication;
            _productApplication = productApplication;
        }

        public void OnGet(CommentSearchModel searchModel)
        {
            ProductItems = _productApplication.Search().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
            Comments = _commentApplication.Search(searchModel);
        }

        public IActionResult OnGetConfirm(long id)
        {
            OperationResult result;
            result = _commentApplication.Confirm(id);
            if (!result.IsSuccedded)
            {
                Message = result.Message;
            }

            return RedirectToPage("./Index");
        }
        public IActionResult OnGetCancel(long id)
        {
            OperationResult result;
            result = _commentApplication.Cancel(id);
            if (!result.IsSuccedded)
            {
                Message = result.Message;
            }

            return RedirectToPage("./Index");
        }
       

       
       
    }
}
