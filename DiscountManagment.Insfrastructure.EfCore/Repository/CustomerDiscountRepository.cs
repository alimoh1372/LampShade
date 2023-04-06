using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscountContracts;
using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;
using SM.Domain.ProductAgg;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class CustomerDiscountRepository:BaseRepository<long,CustomerDiscount>,ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly IProductRepository _productRepository;
        public CustomerDiscountRepository( DiscountContext context, IProductRepository productRepository) : base(context)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                FkProductId = x.FkProductId,
                Reason = x.Reason,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                Id = x.Id
            }).FirstOrDefault();
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel=null)
        {
            var products = _productRepository.Get().Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                Reason = x.Reason,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                FkProductId = x.FkProductId
            });
            if (searchModel==null)
            {
                return query.OrderByDescending(x => x.Id).ToList();
            }

            if (searchModel.FkProductId.HasValue && searchModel.FkProductId!=0)
            {
                query = query.Where(x => x.FkProductId == searchModel.FkProductId);
            }

            if (!string.IsNullOrWhiteSpace( searchModel.StartDate))
            {
                query = query.Where(x => x.StartDate.ToGeorgianDateTime() >= searchModel.StartDate.ToGeorgianDateTime());
            }
            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                query = query.Where(x => x.EndDate.ToGeorgianDateTime() <= searchModel.EndDate.ToGeorgianDateTime());
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Reason))
            {
                query = query.Where(x => x.Reason.Contains(searchModel.Reason));
            }

            var list = query.OrderByDescending(x => x.Id).ToList();
         
            foreach (CustomerDiscountViewModel item in list)
            {
                item.Product = products.FirstOrDefault(x => x.Id == item.FkProductId).Name;
            }

            return list;
        }
    }
}