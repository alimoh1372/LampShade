using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductContracts;
using SM.Domain.ProductAgg;
using SM.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.ProductApplication
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUpload _fileUpload;
        public ProductApplication(IProductRepository productRepository, IFileUpload fileUpload, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUpload = fileUpload;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            OperationResult result = new OperationResult();
            if (_productRepository.IsExists(x => x.Name == command.Name && x.Code == command.Code))
            {
                result.Failed(ApplicationMessage.Duplication);
                return result;
            }

            string slug = command.Slug.Slugify();
            string productCategorySlug = _productCategoryRepository.GetSlugBy(command.FkCategoryId);
            string basePath = $"UploadedFiles/ProductCategory/{productCategorySlug}/{slug}";
            string picture = _fileUpload.UploadFile(command.Picture, basePath);
            Product product = new Product(command.Name, command.Code, command.ShortDescription,
                command.ShortDescription,
               picture, command.PictureAlt, command.PictureTitle, slug, command.Keywords,
                command.MetaDescription, command.FkCategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();

            return result.Succedded();

        }

        public OperationResult Edit(EditProduct command)
        {
            OperationResult result = new OperationResult();
            Product product = _productRepository.GetProductWithProductCategoryBy(command.Id);
            string pictureBeforUpdatePath = product.Picture;
            if (product == null)
            {
                result.Failed(ApplicationMessage.NotFound);
                return result;
            }

            if (_productRepository.IsExists(x => x.Name == command.Name && x.Code == command.Code && x.Id != command.Id))
            {
                result.Failed(ApplicationMessage.Duplication);
                return result;
            }


            string slug = command.Slug.Slugify();
            string basePath = $"UploadedFiles/ProductCategory/{product.ProductCategory.Slug}/{slug}";
            string picture = _fileUpload.UploadFile(command.Picture, basePath);
            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.ShortDescription,
                picture, command.PictureAlt, command.PictureTitle, slug, command.Keywords,
                command.MetaDescription, command.FkCategoryId);
            _productRepository.SaveChanges();
            _fileUpload.DeleteFile(pictureBeforUpdatePath);
            return result.Succedded();
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);

        }



        public List<ProductViewModel> Search(ProductSearchModel searchModel=null)
        {
            return _productRepository.Search(searchModel);
        }

       
     
    }
}