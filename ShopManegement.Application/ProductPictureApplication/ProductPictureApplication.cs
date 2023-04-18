using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPictureContracts;
using ShopManagement.Application.Contracts.SlideContracts;
using SM.Domain.ProductAgg;
using SM.Domain.ProductPictureAgg;

namespace ShopManagement.Application.ProductPictureApplication
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUpload _fileUpload;
        private readonly IProductRepository _productRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IFileUpload fileUpload, IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUpload = fileUpload;
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            OperationResult result = new OperationResult();
            if (_productPictureRepository.IsExists(x => command.Picture.FileName.Contains(x.Picture) && x.FkProductId == command.FkProductId))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            Product product = _productRepository.GetProductWithProductCategoryBy(command.FkProductId);
            
            string slug = product.Slug;
            string basePath = $"UploadedFiles/ProductCategory/{product.ProductCategory.Slug}/{slug}/ProductPictures";
            string picture = _fileUpload.UploadFile(command.Picture, basePath);
            ProductPicture productPicture = new ProductPicture(picture, command.PictureAlt,
                    command.PictureTitle, command.FkProductId);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            OperationResult result = new OperationResult();
            ProductPicture productPicture = null;
            string fileName = command.Picture.FileName;
            if (_productPictureRepository.IsExists(x =>  command.Picture.FileName.Contains(x.Picture) && x.FkProductId == command.FkProductId && x.Id != command.Id))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }
            productPicture = _productPictureRepository.GetProductPictureWithProductAndProductCategoryBy(command.Id);
           
            if (productPicture == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }

            string productPictureBeforeUpdate = productPicture.Picture;
            string slug = productPicture.Product.Slug;
            string basePath = $"UploadedFiles/ProductCategory/{productPicture.Product.ProductCategory.Slug}/{slug}/ProductPictures";
            string picture = _fileUpload.UploadFile(command.Picture, basePath);

            productPicture.Edit(picture, command.PictureAlt,
                command.PictureTitle, command.FkProductId);
            _productPictureRepository.SaveChanges();
            _fileUpload.DeleteFile(productPictureBeforeUpdate);
            return result.Succedded();
        }

        public OperationResult Remove(long id)
        {
            OperationResult result = new OperationResult();
            ProductPicture productPicture = null;
            if (!_productPictureRepository.IsExists(x=> x.Id == id))
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            productPicture.Remove();
            _productPictureRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Active(long id)
        {

            OperationResult result = new OperationResult();
            ProductPicture productPicture = null;
            if (!_productPictureRepository.IsExists(x => x.Id == id))
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            productPicture.Active();
            _productPictureRepository.SaveChanges();
            return result.Succedded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel = null)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}