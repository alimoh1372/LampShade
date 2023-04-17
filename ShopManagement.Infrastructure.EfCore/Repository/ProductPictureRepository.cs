using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPictureContracts;
using ShopManagement.Application.Contracts.SlideContracts;
using SM.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductPictureRepository:BaseRepository<long,ProductPicture>,IProductPictureRepository
    {
        private readonly ShopContext _context;
        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            EditProductPicture editProduct = _context.ProductPictures
                .Select(x => new EditProductPicture
                {
                    Picture = x.Picture,
                    PictureAlt =x.PictureAlt,
                    PictureTitle =x.PictureTitle,
                    FkProductId = x.FkProductId,
                    Id =x.Id
                })
                .FirstOrDefault(x => x.Id == id);
            return editProduct;

        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel = null)
        {
            var query = _context.ProductPictures.Include(x => x.Product)
                .Select(x => new ProductPictureViewModel
                {
                    Id = x.Id,
                    Picture =x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    CreationDate = x.CreationDate.ToFarsi(),
                    FkProductId=x.FkProductId,
                    IsRemoved = x.IsRemoved,
                    Product = x.Product.Name
                });
            if (searchModel==null)
                return query.OrderByDescending(x => x.Id).ToList();

            if (searchModel.FkProductId !=0)
                query = query.Where(x => x.FkProductId == searchModel.FkProductId);
            if (!string.IsNullOrWhiteSpace(searchModel.PictureAlt))
                query = query.Where(x => x.PictureAlt == searchModel.PictureAlt);
            if (!string.IsNullOrWhiteSpace(searchModel.PictureTitle))
                query = query.Where(x => x.PictureTitle == searchModel.PictureTitle);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}