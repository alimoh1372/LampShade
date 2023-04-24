using System.Collections.Generic;
using System.Linq;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampshadeQuery.Query
{
    public class ArticleCategoryQuery:IArticleCategoryQuery
    {
        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategoriesForMenu()
        {
            return _context.ArticleCategories.Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ArticleCounts = x.Articles.Count,
                    Articles = x.Articles.Where(y=>y.FkArticleCategoryId==x.Id)
                        .Select(y=>new ArticleQueryModel
                        {
                            Title = y.Title,
                            Slug = y.Slug,
                        }).ToList()
                }).OrderByDescending(x=>x.Id).ToList();
        }
    }
}