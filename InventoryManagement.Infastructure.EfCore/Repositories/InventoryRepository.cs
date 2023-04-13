using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
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
                CurrentCount = x.CalculateCurrentCount(),
                CreationDate=x.CreationDate.ToFarsi()
            });

            if (searchModel.FkProductId>0)
            {
                query = query.Where(x => x.FkProductId == searchModel.FkProductId);
            }

            if (searchModel.UnitPrice > 0)
            {
                query = query.Where(x => x.UnitPrice == searchModel.UnitPrice);
            }

            if (searchModel.NotInStock ^ searchModel.IsInStock )
            {
                query = searchModel.IsInStock ? query.Where(x => x.IsInStock == true) : query.Where(x => x.IsInStock == false);
            }
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
          return  _context.Inventory.FirstOrDefault(x => x.FkProductId == productId);
        }

        public List<InventoryOperationsViewModel> GetInventoryOperations(long inventoryId)
        {
            Inventory inventory = Get(inventoryId);
           return inventory.InventoryOperations.Select(x => new InventoryOperationsViewModel
            {
                Id = x.Id,
                Operation = x.Operation,
                Count = x.Count,
                FkOperatorId = x.FkOperatorId,
                Operator = "مدیر سیستم",
                OperationDate = x.OperationDate.ToFarsi(),
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                OrderId = x.OrderId
            }).OrderByDescending(x => x.Id).ToList();
           
        }
    }
}