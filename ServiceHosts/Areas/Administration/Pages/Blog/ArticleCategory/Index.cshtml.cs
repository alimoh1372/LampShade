using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategoryContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryContracts;

namespace ServiceHosts.Areas.Administration.Pages.Blog.ArticleCategory
{
    public class IndexModel : PageModel
    {
        public List<ArticleCategoryViewModel> ArticleCategoryViewModels { get; set; }
       
        public ArticleCategorySearchModel SearchModel { get; set; }
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
           
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategoryViewModels = _articleCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("RegisterAccount", new CreateArticleCategory());
        }

        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            EditArticleCategory editArticleCategory = _articleCategoryApplication.GetDetails(id);
            return Partial("Edit", editArticleCategory);
        }

        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            bool isValid = ModelState.IsValid;
            if (isValid)
            {
                
            }
            OperationResult result = _articleCategoryApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
