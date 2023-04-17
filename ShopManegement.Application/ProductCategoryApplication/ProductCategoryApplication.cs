using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using SM.Domain.ProductCategoryAgg;

namespace ShopManagement.Application.ProductCategoryApplication
{
    public class ProductCategoryApplication :IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUpload _fileUpload;
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUpload fileUpload)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUpload = fileUpload;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            OperationResult operationResult = new OperationResult();

            if (_productCategoryRepository.IsExists(x=>x.Name==command.Name))
            {
                operationResult.Failed(ApplicationMessage.Duplication);
                return operationResult;
            }

            string slug = command.Slug.Slugify();
            string path = $"UploadedFiles/ProductCategory/{slug}";
            string picture = _fileUpload.UploadFile(command.Picture, path);
            ProductCategory productCategory = new ProductCategory(command.Name, command.Description, picture
                , command.PictureAlt, command.PictureTitle, command.Keywords, command.MetaDescription, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();
          return  operationResult.Succedded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            OperationResult operationResult = new OperationResult();
            ProductCategory productCategory = _productCategoryRepository.Get(command.Id);
            var lastPictureBeforUpdate = productCategory.Picture;
            if (productCategory == null)
                return operationResult.Failed(
                    "گروه محصولی با مشخصات درخواستی وجود ندارد لطفا بررسی و مجددا امتحان نمائید.");
            if (_productCategoryRepository.IsExists(x => x.Name == command.Name & x.Id != command.Id))
                return operationResult.Failed(
                    "امکان ثبت گروه محصولات با عنوان(نام)تکراری وجود ندارد.لطفا مجددا تلاش بفرمائید.");
            string slug = command.Slug.Slugify();
            string path = $"UploadedFiles/ProductCategory/{slug}";
            string picture = _fileUpload.UploadFile(command.Picture, path);
            productCategory.Edit(command.Name,command.Description,picture,command.PictureAlt,command.PictureTitle
                ,command.Keywords,command.MetaDescription,slug);
            _productCategoryRepository.SaveChanges();
            _fileUpload.DeleteFile(lastPictureBeforUpdate);
            return operationResult.Succedded();
            
        }

        public EditProductCategory GetDetails(long id)
        {
           return _productCategoryRepository.GetDetails(id);
        }

        public List<ProductCategoryViewModel> Search(SearchProductCategoryModel searchModel=null)
        {
          return  _productCategoryRepository.Search(searchModel);
        }
    }
}