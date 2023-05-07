using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
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
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Articles = x.Articles.Where(y=>y.FkArticleCategoryId==x.Id)
                        .Select(y=>new ArticleQueryModel
                        {
                            Title = y.Title,
                            Slug = y.Slug,
                        }).ToList()
                }).OrderByDescending(x=>x.Id).ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategoryWithArticlesBy(string slug)
        {
            return _context.ArticleCategories
                .Include(x=>x.Articles)
                .Select(x => new ArticleCategoryQueryModel
            {
                Name = x.Name,
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Description = x.Description,
                ShowOrder = x.ShowOrder,
                Keywords=x.Keywords,
                Slug = x.Slug,
                ArticleCounts = x.Articles.Count,
                Id = x.Id,
                Articles =MapArticles(x.Articles),
                KeywordList = GetKeywordList(x.Keywords)
            }).FirstOrDefault(x => x.Slug == slug);
        }

        private static List<string> GetKeywordList(string keywords)
        {
            return keywords.Split(",").ToList();
        }

        private static List<ArticleQueryModel> MapArticles(ICollection<Article> Articles)
        {
            return Articles.Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,
                Keywords = x.Keywords,
                KeywordList = x.Keywords.Split(",").ToList(),
            }).ToList();
        }
    }
}