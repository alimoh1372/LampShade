using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleContracts;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleRepository : BaseRepository<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;
        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticle GetDetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress,
                FkArticleCategoryId = x.FkArticleCategoryId,
                Id = x.Id,
                PicturePath = x.Picture
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles
                .Include(x => x.ArticleCategory)
                .Select(x => new ArticleViewModel
                {
                    Id=x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Category = x.ArticleCategory.Name,
                    FkArticleCategoryId = x.FkArticleCategoryId
                });
            if (string.IsNullOrWhiteSpace(searchModel.Title))
            {
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            }

            if (searchModel.FkArticleCategoryId is >0)
            {
                query = query.Where(x => x.FkArticleCategoryId == searchModel.FkArticleCategoryId);
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public Article GetArticleWithArticleCategory(long id)
        {
            return _context.Articles.Include(x => x.ArticleCategory).FirstOrDefault(x => x.Id == id);
        }
    }
}