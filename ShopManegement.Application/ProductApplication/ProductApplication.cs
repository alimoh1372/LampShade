using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductContracts;
using SM.Domain.ProductAgg;

namespace ShopManagement.Application.ProductApplication
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            OperationResult result = new OperationResult();
            if (_productRepository.IsExists(x => x.Name == command.Name || x.Code == command.Code))
            {
                result.Failed(ApplicationMessage.Duplication);
                return result;
            }

            string slug = command.Slug.Slugify();
            Product product = new Product(command.Name, command.Code, command.ShortDescription,
                command.ShortDescription,
                command.Picture, command.PictureAlt, command.PictureTitle, slug, command.Keywords,
                command.MetaDescription, command.FkCategoryId);
            _productRepository.Create(product);
            _productRepository.SaveChanges();

            return result.Succedded();

        }

        public OperationResult Edit(EditProduct command)
        {
            OperationResult result = new OperationResult();
            Product product = _productRepository.Get(command.Id);
            if (product == null)
            {
                result.Failed(ApplicationMessage.NotFound);
                return result;
            }

            if (_productRepository.IsExists(x => x.Name == command.Name || x.Code == command.Code && x.Id != command.Id))
            {
                result.Failed(ApplicationMessage.Duplication);
                return result;
            }


            string slug = command.Slug.Slugify();
            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.ShortDescription,
                command.Picture, command.PictureAlt, command.PictureTitle, slug, command.Keywords,
                command.MetaDescription, command.FkCategoryId);
            _productRepository.SaveChanges();

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