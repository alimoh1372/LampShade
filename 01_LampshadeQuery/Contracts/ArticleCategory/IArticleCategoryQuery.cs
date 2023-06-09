﻿using System.Collections.Generic;

namespace _01_LampshadeQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        List<ArticleCategoryQueryModel> GetArticleCategoriesForMenu();
        ArticleCategoryQueryModel GetArticleCategoryWithArticlesBy(string slug);
    }
}