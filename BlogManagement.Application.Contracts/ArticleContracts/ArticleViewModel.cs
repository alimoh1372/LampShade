namespace BlogManagement.Application.Contracts.ArticleContracts
{
    public class ArticleViewModel
    {
        public long Id { get; set; }

        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }
        public string PublishDate { get; set; }

        public string Category { get; set; }
        public long FkArticleCategoryId { get; set; }
      
    }
}