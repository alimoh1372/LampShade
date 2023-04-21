using System.Collections.Generic;
using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategoryContracts;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository:IBaseRepository<long,ArticleCategory>
    {
        EditArticleCategory GetDetails(long id);

        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}