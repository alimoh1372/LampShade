using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountInventory;
        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountInventory)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountInventory = discountInventory;
        }

        public List<ProductCategoryQueryModel> GetProductCategoryQueryModel()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProductsQueryModel()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new
            {
                x.FkProductId,
                x.UnitPrice,
                x.IsInStock
            }).ToList();
            var discounts = _discountInventory.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new
                {
                    x.FkProductId,
                    x.DiscountRate
                }).ToList();
            var productCategories = _context.ProductCategories
                 .Include(x => x.Products)
                 .ThenInclude(x => x.ProductCategory)
                 .Select(x => new ProductCategoryQueryModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Picture = x.Picture,
                     PictureAlt = x.PictureAlt,
                     PictureTitle = x.PictureTitle,
                     Slug = x.Slug,
                     Products = x.Products.Select(y => new ProductQueryModel
                     {
                         Id = y.Id,
                         Name = y.Name,
                         ShortDescription = y.ShortDescription,
                         Picture = y.Picture,
                         PictureAlt = y.PictureAlt,
                         PictureTitle = y.PictureTitle,
                         Slug = y.Slug,
                         Category = y.ProductCategory.Name,
                         //UnitPrice = inventory.FirstOrDefault(inv => inv.FkProductId == y.Id).UnitPrice,
                         //IsInStock = inventory.FirstOrDefault(inv => inv.FkProductId == y.Id).IsInStock,
                         //DiscountRate = discounts.FirstOrDefault(dis => y.Id == dis.FkProductId).DiscountRate>0?,
                         //PriceWithDiscount = discounts.FirstOrDefault(dis => y.Id == dis.FkProductId).DiscountRate,
                     }).ToList()
                 }).ToList();
            foreach (ProductCategoryQueryModel productCategory in productCategories)
            {
                foreach (ProductQueryModel product in productCategory.Products)
                {
                    var productInventory = inventory.FirstOrDefault(inv => inv.FkProductId == product.Id);
                    var productDiscount = discounts.FirstOrDefault(x => x.FkProductId == product.Id);
                    if (productInventory!=null)
                    {
                        product.IsInStock = productInventory.IsInStock;
                        product.UnitPrice = (productInventory.UnitPrice>0)?productInventory.UnitPrice.ToMoney():string.Empty;
                    }
                    else
                    {
                        product.IsInStock = false;
                        product.UnitPrice = string.Empty;
                    }

                    if (!string.IsNullOrWhiteSpace(product.UnitPrice) && product.IsInStock)
                    {
                        if (productDiscount != null)
                        {
                            if (productDiscount.DiscountRate > 0)
                            {
                                product.HasDiscount = true;
                                product.DiscountRate = productDiscount.DiscountRate;
                                product.PriceWithDiscount =Math.Round( (productInventory.UnitPrice-productInventory.UnitPrice * product.DiscountRate / 100)).ToMoney();
                            }
                            else
                            {
                                product.HasDiscount = false;
                                product.DiscountRate = 0;
                                product.PriceWithDiscount = string.Empty;
                            }
                        }
                        else
                        {
                            product.HasDiscount = false;
                            product.DiscountRate = 0;
                        }
                    }
                    else
                    {
                        product.HasDiscount = false;
                        product.DiscountRate = 0;
                    }
                }
            }

            return productCategories;
        }

        public ProductCategoryQueryModel GetProductCategoryQueryModelBy(string slug)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new
            {
                x.FkProductId,
                x.UnitPrice,
                x.IsInStock
            }).ToList();
            var discounts = _discountInventory.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new
                {
                    x.FkProductId,
                    x.DiscountRate,
                    x.EndDate
                }).ToList();
            var productCategory = _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.ProductCategory)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Description = x.Description,
                    Products = x.Products.Select(y => new ProductQueryModel
                    {
                        Id = y.Id,
                        Name = y.Name,
                        ShortDescription = y.ShortDescription,
                        Picture = y.Picture,
                        PictureAlt = y.PictureAlt,
                        PictureTitle = y.PictureTitle,
                        Slug = y.Slug,
                        Category = y.ProductCategory.Name,
                        
                        //UnitPrice = inventory.FirstOrDefault(inv => inv.FkProductId == y.Id).UnitPrice,
                        //IsInStock = inventory.FirstOrDefault(inv => inv.FkProductId == y.Id).IsInStock,
                        //DiscountRate = discounts.FirstOrDefault(dis => y.Id == dis.FkProductId).DiscountRate>0?,
                        //PriceWithDiscount = discounts.FirstOrDefault(dis => y.Id == dis.FkProductId).DiscountRate,
                    }).ToList()
                }).FirstOrDefault(x => x.Slug == slug);
           
                foreach (ProductQueryModel product in productCategory.Products)
                {
                    var productInventory = inventory.FirstOrDefault(inv => inv.FkProductId == product.Id);
                    var productDiscount = discounts.FirstOrDefault(x => x.FkProductId == product.Id);
                    if (productInventory != null)
                    {
                        product.IsInStock = productInventory.IsInStock;
                        product.UnitPrice = (productInventory.UnitPrice > 0) ? productInventory.UnitPrice.ToMoney() : string.Empty;
                    }
                    else
                    {
                        product.IsInStock = false;
                        product.UnitPrice = string.Empty;
                    }

                    if (!string.IsNullOrWhiteSpace(product.UnitPrice) && product.IsInStock)
                    {
                        if (productDiscount != null)
                        {
                            if (productDiscount.DiscountRate > 0)
                            {
                                product.HasDiscount = true;
                                product.DiscountRate = productDiscount.DiscountRate;
                                product.PriceWithDiscount = Math.Round((productInventory.UnitPrice - productInventory.UnitPrice * product.DiscountRate / 100)).ToMoney();
                                product.DiscountExpireDate = productDiscount.EndDate.ToDiscountDate();
                            }
                            else
                            {
                                product.HasDiscount = false;
                                product.DiscountRate = 0;
                                product.PriceWithDiscount = string.Empty;
                            }
                        }
                        else
                        {
                            product.HasDiscount = false;
                            product.DiscountRate = 0;
                        }
                    }
                    else
                    {
                        product.HasDiscount = false;
                        product.DiscountRate = 0;
                    }
                }
            

            return productCategory;
        }
    }
}