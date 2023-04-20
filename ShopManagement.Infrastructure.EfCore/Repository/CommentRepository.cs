using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.CommentContracts;
using SM.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository : BaseRepository<long, Comment>, ICommentRepository
    {
        private readonly ShopContext _context;
        public CommentRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments
                .Include(x => x.Product)
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    CreationDate = x.CreationDate.ToFarsi(),
                    FkProductId = x.FkProductId,
                    Email = x.Email,
                    IsConfirmed = x.IsConfirmed,
                    Product = x.Product.Name
                });
            if (searchModel.FkProductId is > 0)
            {
                query = query.Where(x => x.FkProductId == searchModel.FkProductId);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Message))
            {
                query = query.Where(x => x.Message.Contains(searchModel.Email));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Email));
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}