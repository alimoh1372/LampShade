using System.Security.Cryptography.X509Certificates;

namespace _01_LampshadeQuery.Contracts.Comment
{
    public interface ICommentQuery
    {
        
    }

    public class CommentQueryModel
    {
        public long Id { get; set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Message { get;  set; }
        public string CreationDate { get; set; }
        public int Point { get;  set; }
        public bool IsConfirmed { get;  set; }

        public long FkProductId { get;  set; }
        
    }
}