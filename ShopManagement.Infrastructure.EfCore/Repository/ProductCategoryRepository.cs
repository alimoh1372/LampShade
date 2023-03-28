using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using SM.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository:IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context)
        {
            _context = context;
        }

        public void Create(ProductCategory entity)
        {
            _context.ProductCategories.Add(entity);
        }

        public ProductCategory Get(long id)
        {
            return _context.ProductCategories.Find(id);
        }

        public List<ProductCategory> GetAll()
        {
          return  _context.ProductCategories.ToList();
        }

        public bool IsExists(Expression<Func<ProductCategory, bool>> predicate)
        {
          return  _context.ProductCategories.Any(predicate);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public List<ProductCategoryViewModel> Search(SearchProductCategoryModel searchModel)
        {
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                CreationDate = x.CreationDate.ToString(CultureInfo.InvariantCulture),
                Name = x.Name,
                Picture = x.Picture
            });
            query = query.Where(x => x.Name.Contains(searchModel.Name)).OrderByDescending(x=>x.Id);
            return query.ToList();
        }

    }
}