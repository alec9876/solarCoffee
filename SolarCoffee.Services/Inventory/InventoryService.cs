using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDBContext _context;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(SolarDBContext context, ILogger<InventoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public ProductInventory GetByProductId(int productId)
        {
            return _context.ProductInventories
                .Include(pi => pi.InventoryProduct)
                .FirstOrDefault(pi => pi.InventoryProduct.Id == productId);
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return _context.ProductInventories
                .Include(pi => pi.InventoryProduct)
                .Where(pi => !pi.InventoryProduct.IsArchived)
                .ToList();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);

            return _context.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap => snap.SnapshotTime > earliest && !snap.Product.IsArchived)
                .ToList();
        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            var now = DateTime.UtcNow;
            try
            {
                var inventory = _context.ProductInventories
                    .Include(inv => inv.InventoryProduct)
                    .First(inventory => inventory.InventoryProduct.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot(inventory);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error creating snapshot: {ex.Message}");
                }

                _context.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Product {id} inventory adjusted",
                    Time = now
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"Error updating QuantityOnHand, {ex.StackTrace}",
                    Time = now
                };
            }
        }

        private void CreateSnapshot(ProductInventory inventory)
        {
            var now = DateTime.UtcNow;
            var snapshot = new ProductInventorySnapshot
            {
                SnapshotTime = now,
                Product = inventory.InventoryProduct,
                QuantityOnHand = inventory.QuantityOnHand
            };

            _context.Add(snapshot);
        }
    }
}
