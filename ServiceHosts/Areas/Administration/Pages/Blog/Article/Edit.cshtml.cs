using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Application.Contracts.ArticleCategoryContracts;
using BlogManagement.Application.Contracts.ArticleContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHosts.Areas.Administration.Pages.Blog.Article
{
    public class EditModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public EditArticle Command { get; set; }
        public SelectList ArticleCategoryItems { get; set; }
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public EditModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet(long id)
        {
            Command = _articleApplication.GetDetails(id);
            ArticleCategoryItems = new SelectList(_articleCategoryApplication.GetSelectList(), "Id", "Name");
        }
        public RedirectToPageResult OnPost(EditArticle command)
        {
            if (ModelState.IsValid)
            {

            }

            var result = _articleApplication.Edit(command);
            if (!result.IsSuccedded)
            {
                Message = result.Message;
                return RedirectToPage(this);
            }

            return RedirectToPage("./Index");
        }
    }
}
