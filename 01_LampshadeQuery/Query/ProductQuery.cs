using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using _01_LampshadeQuery.Contracts.ProductPicture;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace _01_LampshadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountInventory;
        public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountInventory)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountInventory = discountInventory;
        }

        public ProductQueryModel GetProductDetailsBy(string slug)
        {
            double unitPrice;
            var product = _context.Products
                .Include(x => x.ProductCategory)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel
                {

                    Id = x.Id,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Code = x.Code,
                    Category = x.ProductCategory.Name,
                    CategorySlug = x.ProductCategory.Slug,
                    ProductPictures = x.ProductPictures.Select(y => new ProductPictureQueryModel
                    {
                        Picture = y.Picture,
                        PictureAlt = y.PictureAlt,
                        PictureTitle = y.PictureTitle,
                        IsRemoved = y.IsRemoved,
                        FkProductId = x.Id
                    })
                        .Where(y => y.FkProductId == x.Id).ToList()
                })
                .FirstOrDefault(x => x.Slug == slug);


            if (product != null)
            {
                var productInventory = _inventoryContext.Inventory.Select(x => new
                {
                    x.FkProductId,
                    x.UnitPrice,
                    x.IsInStock,
                    CurrentCount = x.CalculateCurrentCount()
                }).FirstOrDefault(inv => inv.FkProductId == product.Id);
                var productDiscount = _discountInventory.CustomerDiscounts
                    .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                    .Select(x => new
                    {
                        x.FkProductId,
                        x.DiscountRate,
                        x.EndDate
                    }).FirstOrDefault(dis => dis.FkProductId == product.Id);

                if (productInventory != null)
                {
                    unitPrice = (productInventory.UnitPrice > 0) ? productInventory.UnitPrice :0;
                    product.IsInStock = productInventory.IsInStock;
                    product.UnitPrice = (productInventory.UnitPrice > 0) ? productInventory.UnitPrice.ToMoney() : string.Empty;
                    product.CurrentCount = productInventory.CurrentCount.ToString("##,###");
                }
                else
                {
                    product.IsInStock = false;
                    product.UnitPrice = string.Empty;
                }

                if (!string.IsNullOrWhiteSpace(product.UnitPrice) && product.IsInStock && productDiscount != null)
                {
                    if (productDiscount.DiscountRate > 0)
                    {
                        var discountedPrice =
                            Math.Round(
                            productInventory.UnitPrice
                            - (productInventory.UnitPrice * productDiscount.DiscountRate / 100)
                            );

                        product.HasDiscount = true;
                        product.DiscountRate = productDiscount.DiscountRate;
                        product.PriceWithDiscount = discountedPrice.ToMoney();
                        product.DiscountPoint = Math.Round(productInventory.UnitPrice - discountedPrice).ToMoney();
                        product.DiscountExpireDate = productDiscount.EndDate.ToDiscountDate();
                    }
                    else
                    {
                        product.HasDiscount = false;
                        product.DiscountRate = 0;
                        product.PriceWithDiscount = string.Empty;
                        product.DiscountPoint = string.Empty;
                    }


                }
                else
                {
                    product.HasDiscount = false;
                    product.DiscountRate = 0;
                }
            }


            return product;
        }

        public List<ProductQueryModel> GetLatestArrivals(int count = 6)
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
            var products = _context.Products
                .Include(x => x.ProductCategory)
                .Select(x => new ProductQueryModel
                {

                    Id = x.Id,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Category = x.ProductCategory.Name,
                    //UnitPrice = inventory.FirstOrDefault(inv => inv.FkProductId == y.Id).UnitPrice,
                    //IsInStock = inventory.FirstOrDefault(inv => inv.FkProductId == y.Id).IsInStock,
                    //DiscountRate = discounts.FirstOrDefault(dis => y.Id == dis.FkProductId).DiscountRate>0?,
                    //PriceWithDiscount = discounts.FirstOrDefault(dis => y.Id == dis.FkProductId).DiscountRate,
                }).OrderByDescending(x => x.Id).Take(count).ToList();


            foreach (ProductQueryModel product in products)
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

                if (!string.IsNullOrWhiteSpace(product.UnitPrice) && product.IsInStock && productDiscount != null)
                {
                    if (productDiscount.DiscountRate > 0)
                    {
                        product.HasDiscount = true;
                        product.DiscountRate = productDiscount.DiscountRate;
                        product.PriceWithDiscount = Math.Round((productInventory.UnitPrice - productInventory.UnitPrice * product.DiscountRate / 100)).ToMoney();
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


            return products;
        }

        public List<ProductQueryModel> GetProductQueryModelsBy(string value)
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
            var products = _context.Products
                .Include(x => x.ProductCategory)
                .Select(x => new ProductQueryModel
                {

                    Id = x.Id,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Category = x.ProductCategory.Name,
                    //UnitPrice = inventory.FirstOrDefault(inv => inv.FkProductId == y.Id).UnitPrice,
                    //IsInStock = inventory.FirstOrDefault(inv => inv.FkProductId == y.Id).IsInStock,
                    //DiscountRate = discounts.FirstOrDefault(dis => y.Id == dis.FkProductId).DiscountRate>0?,
                    //PriceWithDiscount = discounts.FirstOrDefault(dis => y.Id == dis.FkProductId).DiscountRate,
                })
                .Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value))
                .OrderByDescending(x => x.Id).ToList();


            foreach (ProductQueryModel product in products)
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

                if (!string.IsNullOrWhiteSpace(product.UnitPrice) && product.IsInStock && productDiscount != null)
                {
                    if (productDiscount.DiscountRate > 0)
                    {
                        product.HasDiscount = true;
                        product.DiscountRate = productDiscount.DiscountRate;
                        product.PriceWithDiscount = Math.Round((productInventory.UnitPrice - productInventory.UnitPrice * product.DiscountRate / 100)).ToMoney();
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


            return products;
        }
    }
}