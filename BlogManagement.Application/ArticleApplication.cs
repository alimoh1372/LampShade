using System;
using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleContracts;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication:IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUpload _fileUpload;

        public ArticleApplication(IArticleRepository articleRepository, IFileUpload fileUpload, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUpload = fileUpload;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            OperationResult result = new OperationResult();
            if (_articleRepository.IsExists(x=>x.Title ==command.Title && x.FkArticleCategoryId==command.FkArticleCategoryId))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }
            string slug = command.Slug.Slugify();
            string categorySlug = _articleCategoryRepository.GetSlugBy(command.FkArticleCategoryId);
            string basePath = $"UploadedFiles/ArticleCategory/{categorySlug}/{slug}";
            string picture = _fileUpload.UploadFile(command.Picture, basePath);
            DateTime publishDate = command.PublishDate.ToGeorgianDateTime();
            Article article = new Article(command.Title, command.ShortDescription, command.Description, picture,
                command.PictureAlt, command.PictureTitle
                , publishDate, command.FkArticleCategoryId, command.Slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress);
            _articleRepository.Create(article);
            _articleRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Edit(EditArticle command)
        {
            OperationResult result = new OperationResult();
            if (_articleRepository.IsExists(x => x.Title == command.Title && x.FkArticleCategoryId == command.FkArticleCategoryId && x.Id !=command.Id))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            Article article = _articleRepository.GetArticleWithArticleCategory(command.Id);
            if (article == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            string slug = command.Slug.Slugify();
            string categorySlug = article.ArticleCategory.Slug;
            string basePath = $"UploadedFiles/ArticleCategory/{categorySlug}/{slug}/";
            string picture = _fileUpload.UploadFile(command.Picture, basePath);
            DateTime publishDate = command.PublishDate.ToGeorgianDateTime();
            string pictureBeforeUpdate = article.Picture;
            article.Edit(command.Title, command.ShortDescription, command.Description, picture,
                command.PictureAlt, command.PictureTitle
                , publishDate, command.FkArticleCategoryId, command.Slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress);
            _articleRepository.SaveChanges();
            if (!string.IsNullOrWhiteSpace(picture))
            {
                _fileUpload.DeleteFile(pictureBeforeUpdate);
            }
            return result.Succedded();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}