    namespace ShopManagement.Application.Contracts.CommentContracts
{
    public class CommentViewModel
    {
        public long Id { get; set; }
       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public string CreationDate { get; set; }
        public bool IsConfirmed { get; set; }
        public string Point { get; set; }
        public long  FkProductId { get; set; }
        public string Product { get; set; }
        
    }
}