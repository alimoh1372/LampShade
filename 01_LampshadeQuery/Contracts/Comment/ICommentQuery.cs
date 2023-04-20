namespace _01_LampshadeQuery.Contracts.Comment
{
    public interface ICommentQuery
    {
        
    }

    public class CommentQueryModel
    {
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Message { get;  set; }
        public int Point { get;  set; }
        public bool IsConfirmed { get;  set; }

        public long FkProductId { get;  set; }
    }
}