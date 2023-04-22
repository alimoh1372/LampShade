using System.Collections.Generic;
using _0_Framework.Domain;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory:SeoProperties
    {
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }
        public string Slug { get; private set; }

        public ICollection<Article> Articles { get; private set; }

        public ArticleCategory(string name, string picture,string pictureTitle,string pictureAlt, string description, int showOrder, string slug, 
            string keywords, string metaDescription, string canonicalAddress) :
            base(keywords,metaDescription, canonicalAddress)
        {
            Name = name;
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            
        }
        public void Edit(string name, string picture, string pictureTitle, string pictureAlt, string description, int showOrder, string slug,
                string keywords, string metaDescription, string canonicalAddress)
        {
            base.Edit(keywords, metaDescription, canonicalAddress);
            Name = name;
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
        }
    }
    
}