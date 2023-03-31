﻿using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using SM.Domain.ProductCategoryAgg;

namespace SM.Domain.ProductAgg
{
    public class Product: EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public double UnitPrice { get; private set; }
        public bool IsInStock { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }


        public long FkCategoryId { get; private set; }
        public ProductCategory ProductCategory { get; private set; }

        public Product(string name, string code, double unitPrice, string shortDescription, string description, string picture, string pictureAlt, string pictureTitle, string slug, string keywords, string metaDescription, long fkCategoryId)
        {
            Name = name;
            Code = code;
            UnitPrice = unitPrice;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            FkCategoryId = fkCategoryId;
            IsInStock = true;
        }

        public void Edit(string name, string code, double unitPrice, string shortDescription, string description, string picture, string pictureAlt, string pictureTitle, string slug, string keywords, string metaDescription, long fkCategoryId)
        {
            Name = name;
            Code = code;
            UnitPrice = unitPrice;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            FkCategoryId = fkCategoryId;
        }

        public void ExistInStock()
        {
            IsInStock = true;
        }
        public void RunningOutInStock()
        {
            IsInStock = false;
        }
    }
}