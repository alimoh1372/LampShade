using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategoryContracts;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication:IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUpload _fileUpload;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUpload fileUpload)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUpload = fileUpload;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            OperationResult result = new OperationResult();
            if (_articleCategoryRepository.IsExists(x=>x.Name== command.Name))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            string slug = command.Slug.Slugify();
            string basePath = $"UploadedFiles/ArticleCategory/{slug}";
            string picture = _fileUpload.UploadFile(command.Picture, basePath);
            ArticleCategory articleCategory = new ArticleCategory(command.Name, picture, command.PictureTitle,
                command.PictureAlt, command.Description, command.ShowOrder,
                slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);
            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            OperationResult result = new OperationResult();
            if (_articleCategoryRepository.IsExists(x => x.Name == command.Name && x.Id !=command.Id))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            ArticleCategory articleCategory = _articleCategoryRepository.Get(command.Id);
            if (articleCategory ==null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            string slug = command.Slug.Slugify();
            string pictureBeforeUpdate = articleCategory.Picture;

            string basePath = $"UploadedFiles/ArticleCategory/{slug}/";
            string picture = _fileUpload.UploadFile(command.Picture, basePath);
            articleCategory.Edit(command.Name, picture, command.PictureTitle,
                command.PictureAlt, command.Description, command.ShowOrder,
                slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);
            _articleCategoryRepository.SaveChanges();
            _fileUpload.DeleteFile(pictureBeforeUpdate);
            return result.Succedded();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }

        public List<ArticleCategoryViewModel> GetSelectList()
        {
            return _articleCategoryRepository.GetSelectList();
        }
    }
}