using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.CommentContracts
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        OperationResult Cancel(long id);
        OperationResult Confirm(long id);

        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}