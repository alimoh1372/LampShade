using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using InventoryManagement.ApplicationContracts.InventoryContracts;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using SM.Domain.ProductAgg;

namespace InventoryManagement.Infrastructure.EfCore.Repositories
{
    public class InventoryRepository:BaseRepository<long,Inventory>,IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly IProductRepository _productRepository;
        public InventoryRepository(InventoryContext context, IProductRepository productRepository) : base(context)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory
            {
                FkProductId = x.FkProductId,
                UnitPrice = x.UnitPrice,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {

            var query = _context.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                IsInStock = x.IsInStock,
                FkProductId=x.FkProductId,
                CurrentCount = x.CalculateCurrentCount()
            });

            if (searchModel.FkProductId>0)
            {
                query = query.Where(x => x.FkProductId == searchModel.FkProductId);
            }

            if (searchModel.UnitPrice > 0)
            {
                query = query.Where(x => x.UnitPrice == searchModel.UnitPrice);
            }

            query = query.Where(x => x.IsInStock == searchModel.IsInStock);
            var inventory = query.OrderByDescending(x => x.Id).ToList();
            var products = _productRepository.Get().Select(x => new { x.Id, x.Name }).ToList();
            inventory.ForEach(item =>
            {
                item.Product = products.FirstOrDefault(x => x.Id == item.FkProductId)?.Name;
            });
            return inventory;


        }

        public Inventory GetBy(long productId)
        {
            throw new System.NotImplementedException();
        }
    }
}