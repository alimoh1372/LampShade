using System;
using _0_Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article:SeoProperties
    {
        public string Title { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Slug { get; private set; }

        public long FkArticleCategoryId { get; private set; }
        public ArticleCategory ArticleCategory { get; private set; }
        public Article( string title, string shortDescription, string description, string picture, string pictureAlt, string pictureTitle, DateTime publishDate
            , long fkArticleCategoryId, string slug, string keywords, string metaDescription, string canonicalAddress)
            : base(keywords, metaDescription, canonicalAddress)
        {
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            FkArticleCategoryId = fkArticleCategoryId;
            PublishDate = publishDate;
        }
        public void Edit(string title, string shortDescription, string description, string picture, string pictureAlt, string pictureTitle, DateTime publishDate
            , long fkArticleCategoryId,string slug, string keywords, string metaDescription, string canonicalAddress)
        {
            base.Edit(keywords, metaDescription, canonicalAddress);
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            FkArticleCategoryId = fkArticleCategoryId;
            PublishDate = publishDate;
        }

    }
}