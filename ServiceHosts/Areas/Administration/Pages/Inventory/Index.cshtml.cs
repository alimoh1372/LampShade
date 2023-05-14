using System.Collections.Generic;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.Configuration;
using DiscountManagement.Application.Contracts.ColleagueDiscountContracts;
using InventoryManagement.ApplicationContracts.InventoryContracts;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductContracts;
using Microsoft.AspNetCore.Authorization;

namespace ServiceHosts.Areas.Administration.Pages.Inventory
{
    [Authorize(Roles = Roles.Administrator)]
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
        [NeedsPermission(InventoryPermissions.ListInventory)]
        public void OnGet(InventorySearchModel searchModel)
        {
            var product = _productApplication.Search();
            ProductItems = new SelectList(product, "Id", "Name");
            InventoryViewModels = _inventoryApplication.Search(searchModel);
        }
        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public IActionResult OnGetCreate()
        {

            CreateInventory model = new CreateInventory
            {
                Products = _productApplication.Search()
            };


            return Partial("RegisterAccount", model);
        }
        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public JsonResult OnPostCreate(CreateInventory command)
        {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }
        [NeedsPermission(InventoryPermissions.EditInventory)]
        public IActionResult OnGetEdit(long id)
        {
            EditInventory edit = _inventoryApplication.GetDetails(id);
            edit.Products = _productApplication.Search();
            return Partial("Edit", edit);
        }
        [NeedsPermission(InventoryPermissions.EditInventory)]
        public JsonResult OnPostEdit(EditInventory command)
        {
            OperationResult result = _inventoryApplication.Edit(command);

            return new JsonResult(result);
        }
        [NeedsPermission(InventoryPermissions.Increase)]
        public IActionResult OnGetIncrease(long id)
        {
            IncreaseInventory increaseModel = new IncreaseInventory{InventoryId = id};
          
            return Partial("Increase", increaseModel);
        }
        [NeedsPermission(InventoryPermissions.Increase)]
        public JsonResult OnPostIncrease(IncreaseInventory command)
        {
            OperationResult result = _inventoryApplication.Increase(command);
            if (!result.IsSuccedded)
            {
                Message = ApplicationMessage.OperationFailed;
            }
            return new JsonResult(result);
        }

        [NeedsPermission(InventoryPermissions.Reduce)]
        public IActionResult OnGetReduce(long id)
        {
            ReduceInventory reduceInventory = new ReduceInventory { InventoryId = id };

            return Partial("Reduce", reduceInventory);
        }
        [NeedsPermission(InventoryPermissions.Reduce)]
        public JsonResult OnPostReduce(ReduceInventory command)
        {
            OperationResult result = _inventoryApplication.Reduce(command);
            if (!result.IsSuccedded)
            {
                Message = ApplicationMessage.OperationFailed;
            }
            return new JsonResult(result);
        }
        [NeedsPermission(InventoryPermissions.OperationLog)]
        public IActionResult OnGetLog(long id)
        {
            var inventoryOperations = _inventoryApplication.GetInventoryOperations(id);

            return Partial("InventoryOperations", inventoryOperations);
        }
    }
}
