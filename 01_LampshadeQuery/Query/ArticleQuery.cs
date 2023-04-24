using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampshadeQuery.Query
{
    public class ArticleQuery:IArticleQuery
    {
        private readonly BlogContext _context;

        public ArticleQuery(BlogContext context)
        {
            _context = context;
        }

        public List<ArticleQueryModel> GetLatestArticles(int numberOfArticles = 6)
        {
            return _context.Articles.Include(x => x.ArticleCategory)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Slug = x.Slug
                }).OrderByDescending(x => x.Id)
                .Take(numberOfArticles).ToList();
        }

        public ArticleQueryModel GetArticleBy(string slug)
        {
            var article= _context.Articles.Include(x => x.ArticleCategory)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    FkArticleCategoryId = x.FkArticleCategoryId,
                    ArticleCategory = x.ArticleCategory.Name,
                    ArticleCategorySlug = x.ArticleCategory.Slug
                }).FirstOrDefault(x=>x.Slug==slug);
            if (!string.IsNullOrWhiteSpace( article.Keywords))
            {
                article.KeywordList = article.Keywords.Split(',').ToList();
            }

            return article;
        }
    }
}