using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.CommentContracts;

namespace SM.Domain.CommentAgg
{
    public interface ICommentRepository:IBaseRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}