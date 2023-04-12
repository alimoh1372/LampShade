using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscountContracts;
using InventoryManagement.ApplicationContracts.InventoryContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductContracts;

namespace ServiceHosts.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public List<InventoryViewModel> InventoryViewModels { get; set; }

        public InventorySearchModel SearchModel { get; set; }
        public SelectList ProductItems { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            var product = _productApplication.Search();
            ProductItems = new SelectList(product, "Id", "Name");
            InventoryViewModels = _inventoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {

            CreateInventory model = new CreateInventory
            {
                Products = _productApplication.Search()
            };


            return Partial("Create", model);
        }

        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            EditInventory edit = _inventoryApplication.GetDetails(id);
            edit.Products = _productApplication.Search();
            return Partial("Edit", edit);
        }

        public JsonResult OnPostEdit(EditInventory command)
        {
            OperationResult result = _inventoryApplication.Edit(command);

            return new JsonResult(result);
        }


    }
}
