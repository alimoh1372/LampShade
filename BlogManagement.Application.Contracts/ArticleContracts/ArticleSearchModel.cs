namespace BlogManagement.Application.Contracts.ArticleContracts
{
    public class ArticleSearchModel
    {
        public string Title { get; set; }
        public long? FkArticleCategoryId { get; set; }
    }
}