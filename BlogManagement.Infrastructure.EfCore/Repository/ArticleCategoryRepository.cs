using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategoryContracts;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleCategoryRepository:BaseRepository<long,ArticleCategory>,IArticleCategoryRepository
    {
        private readonly BlogContext _context;
        public ArticleCategoryRepository( BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategory
            {
                Name = x.Name,
                PicturePath=x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Description = x.Description,
                ShowOrder = x.ShowOrder,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.Description,
                CanonicalAddress = x.CanonicalAddress,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id=x.Id,
                Name = x.Name,
                Picture = x.Picture,
                ShowOrder = x.ShowOrder,
                CreationDate=x.CreationDate.ToFarsi(),
                PictureAlt=x.PictureAlt,
                PictureTitle=x.PictureTitle,
                Description=x.Description
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public string GetSlugBy(long id)
        {
           return _context.ArticleCategories
               .Select(x => new { x.Id, x.Slug })
               .FirstOrDefault(x => x.Id==id)?.Slug;
        }
    }
}