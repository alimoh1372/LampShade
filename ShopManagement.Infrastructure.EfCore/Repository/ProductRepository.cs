﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductContracts;
using SM.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductRepository:BaseRepository<long,Product>,IProductRepository
    {
        private readonly ShopContext _context;
        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProduct GetDetails(long id)
        {
          return 
              _context.Products.Select(x => new EditProduct
            {
                Name = x.Name,
                Code = x.Code,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                //Picture = "x.Picture",
                PicturePath =x.Picture, 
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                FkCategoryId = x.FkCategoryId,
                Id = x.Id,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,

            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel=null)
        {
            var query = _context.Products.Include(x => x.ProductCategory)
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    Name = x.Name,
                    Code = x.Code,
                    ProductCategory =x.ProductCategory.Name,
                    CreationDate = x.CreationDate.ToFarsi(),
                    FkProductCategoryId = x.FkCategoryId
                    
                });
            if (searchModel==null)
            {
                return query.OrderByDescending(x => x.Id).ToList();
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));
            if (searchModel.FkCategoryId != 0)
                query = query.Where(x => x.FkProductCategoryId == searchModel.FkCategoryId);

            return query.OrderByDescending(x=>x.Id).ToList();
        }

        public Product GetProductWithProductCategoryBy(long id)
        {
          return  _context.Products.Include(x => x.ProductCategory)
                .FirstOrDefault(x => x.Id == id);
        }

      
    }
}