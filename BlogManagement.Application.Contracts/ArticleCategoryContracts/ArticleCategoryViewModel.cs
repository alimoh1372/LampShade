namespace BlogManagement.Application.Contracts.ArticleCategoryContracts
{
    public class ArticleCategoryViewModel
    {
     
        public string Name { get; set; }
      
        public string Picture { get; set; }


        public int ShowOrder { get; set; }
        public long Id { get; set; }
        public string CreationDate { get; set; }
        public int ArticleCounts { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAlt { get; set; }
        public string Description { get; set; }
    }
}