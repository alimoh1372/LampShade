using System.Collections.Generic;

namespace _01_LampshadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetProductsQueryModels();
        List<ProductQueryModel> GetLatestArrivals(int count);
        List<ProductQueryModel> GetProductQueryModelsBy(string value);
    }
}