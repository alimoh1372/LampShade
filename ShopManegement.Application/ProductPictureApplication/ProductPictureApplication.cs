using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.SlideContracts;
using SM.Domain.ProductPictureAgg;

namespace ShopManagement.Application.ProductPictureApplication
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            OperationResult result = new OperationResult();
            if (_productPictureRepository.IsExists(x => x.Picture == command.Picture && x.FkProductId == command.FkProductId))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }

            ProductPicture productPicture = new ProductPicture(command.Picture, command.PictureAlt,
                    command.PictureTitle, command.FkProductId);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChanges();
            return result.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            OperationResult result = new OperationResult();
            ProductPicture productPicture = null;
            if (_productPictureRepository.IsExists(x => x.Picture == command.Picture && x.FkProductId == command.FkProductId && x.Id != command.Id))
            {
                return result.Failed(ApplicationMessage.Duplication);
            }
            productPicture = _productPictureRepository.Get(command.Id);
            if (productPicture == null)
            {
                return result.Failed(ApplicationMessage.NotFound);
            }
            productPicture.Edit(command.Picture, command.PictureAlt,
                command.PictureTitle, command.FkProductId);
            _productPictureRepository.SaveChanges();
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