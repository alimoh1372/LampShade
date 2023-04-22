using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategoryContracts;

namespace BlogManagement.Application.Contracts.ArticleContracts
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        OperationResult Edit(EditArticle command);
        EditArticle GetDetails(long id);

        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}