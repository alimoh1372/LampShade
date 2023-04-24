using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHosts.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel Article { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleBy(id);
            LatestArticles = _articleQuery.GetLatestArticles();
            ArticleCategories = _articleCategoryQuery.GetArticleCategoriesForMenu();
        }
    }
}
