using System.Collections.Generic;
using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleContracts;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository:IBaseRepository<long,Article>
    {
        EditArticle GetDetails(long id);

        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        Article GetArticleWithArticleCategory(long id);
    }
}