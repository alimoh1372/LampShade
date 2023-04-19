using System.Collections.Generic;

namespace _01_LampshadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProductDetailsBy(string slug);
        List<ProductQueryModel> GetLatestArrivals(int count);
        List<ProductQueryModel> GetProductQueryModelsBy(string value);

    }
}