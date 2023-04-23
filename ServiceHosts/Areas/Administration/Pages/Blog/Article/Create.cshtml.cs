using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategoryContracts;
using BlogManagement.Application.Contracts.ArticleContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHosts.Areas.Administration.Pages.Blog.Article
{
    public class CreateModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CreateArticle Command { get; set; }
        public SelectList ArticleCategoryItems { get; set; }

        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public CreateModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet()
        {
            ArticleCategoryItems = new SelectList(_articleCategoryApplication.GetSelectList(), "Id", "Name");
        }

        public RedirectToPageResult OnPost(CreateArticle command)
        {
            if (ModelState.IsValid)
            {

            }

            var result = _articleApplication.Create(command);
            if (!result.IsSuccedded)
            {
                Message = result.Message;
               return RedirectToPage(this);
            }

            return RedirectToPage("./Index");
        }
    }
}
