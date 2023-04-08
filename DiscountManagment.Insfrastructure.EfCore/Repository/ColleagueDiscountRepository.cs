using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.ColleagueDiscountContracts;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using SM.Domain.ProductAgg;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class ColleagueDiscountRepository:BaseRepository<long,ColleagueDiscount>,IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly IProductRepository _productRepository;
        public ColleagueDiscountRepository(DiscountContext context, IProductRepository productRepository) : base(context)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            
            return _context.ColleagueDiscounts.Select(x => new EditColleagueDiscount
            {
                FkProductId = x.FkProductId,
                Id = x.Id,
                DiscountRate = x.DiscountRate
            }).FirstOrDefault(x => x.Id==id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel )
        {
            var products = _productRepository.Get().Select(x => new
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            var query = _context.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                Id = x.Id,
                FkProductId = x.FkProductId,
                CreationDate = x.CreationDate.ToFarsi(),
                DiscountRate=x.DiscountRate,
                IsActive=x.IsActive
            });

            if (searchModel.DiscountRate.HasValue && searchModel.DiscountRate != 0)
            {
                query = query.Where(x => x.DiscountRate == searchModel.DiscountRate);
            }
            if (searchModel.FkProductId.HasValue && searchModel.FkProductId != 0)
            {
                query = query.Where(x => x.FkProductId == searchModel.FkProductId);
            }
            var list = query.ToList();
            foreach (ColleagueDiscountViewModel item in list)
            {
                item.Product = products.FirstOrDefault(y => y.Id == item.FkProductId).Name;
            }
            return list.OrderByDescending(x=>x.Id).ToList();
        }
    }
}