using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategoryContracts;
using BlogManagement.Application.Contracts.ArticleContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHosts.Areas.Administration.Pages.Blog.Article
{
    public class IndexModel : PageModel
    {
        public List<ArticleViewModel> ArticleViewModels { get; set; }
       
        public ArticleSearchModel SearchModel { get; set; }

        public SelectList ArticleCategoryItems { get; set; }

        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategoryItems =new SelectList( _articleCategoryApplication.GetSelectList(),"Id","Name");
            ArticleViewModels = _articleApplication.Search(searchModel);
        }

    }
}
