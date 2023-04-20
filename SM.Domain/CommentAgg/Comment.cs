using _0_Framework.Domain;
using SM.Domain.ProductAgg;

namespace SM.Domain.CommentAgg
{
    public class Comment:EntityBase
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Message { get; private set; }
        public int Point { get; private set; }
        public bool IsConfirmed { get; private set; }
        
        public long FkProductId { get; private set; }
        public Product Product { get; private set; }

        public Comment(string name, string email, string message, int point, long fkProductId)
        {
            Name = name;
            Email = email;
            Message = message;
            Point = point;
            FkProductId = fkProductId;
            IsConfirmed = false;
        }

        public void Cancel()
        {
            IsConfirmed = false;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }
    }

    
}