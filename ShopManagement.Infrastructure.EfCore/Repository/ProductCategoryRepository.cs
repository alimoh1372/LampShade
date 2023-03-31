using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using SM.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository:BaseRepository<long,ProductCategory>,IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public EditProductCategory GetDetails(long id)
        {
            return _context.ProductCategories
                .Select(x => new EditProductCategory
            {
                Id = x.Id,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> Search(SearchProductCategoryModel searchModel = null)
        {
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                Name = x.Name,
                Picture = x.Picture
            });
            if (searchModel==null)
            {
                return query.OrderByDescending(x => x.Id).ToList();
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            return query.OrderByDescending(x=>x.Id).ToList();
        }

    }
}