using System.Collections.Generic;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShopManagement.Application.Contracts.CommentContracts;
using SM.Domain.CommentAgg;

namespace ShopManagement.Application.CommentApplication
{
    public class CommentApplication:ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            OperationResult result = new OperationResult();

            Comment comment = new Comment(command.Name, command.Email, command.Message, command.Point,
                command.FkProductId);
            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Cancel(long id)
        {
            OperationResult result = new OperationResult();
         Comment comment=   _commentRepository.Get(id);
           
            comment.Cancel();
            _commentRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Confirm(long id)
        {
            OperationResult result = new OperationResult();
            Comment comment = _commentRepository.Get(id);

            comment.Confirm();
            _commentRepository.SaveChanges();
            return result.Succedded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }
    }
}