using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDBContext _context;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<OrderService> _logger;
        public OrderService(SolarDBContext context, IProductService productService, 
            ILogger<OrderService> logger, IInventoryService inventoryService)
        {
            _context = context;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            _logger.LogInformation("Generating new order");

            foreach (var item in order.SalesOrderItems)
            {
                item.InventoryProduct = _productService.GetProductById(item.InventoryProduct.Id);

                var inventoryId = _inventoryService.GetByProductId(item.InventoryProduct.Id).Id;

                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            };

            try
            {
                _context.SalesOrders.Add(order);
                _context.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = $"Open Order Created",
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = $"Failed to generate invoice: {ex.StackTrace}",
                    Time = DateTime.UtcNow
                };
            }
        }

        public List<SalesOrder> GetOrders()
        {
            return _context.SalesOrders
                .Include(so => so.Customer)
                    .ThenInclude(c => c.PrimaryAddress)
                .Include(so => so.SalesOrderItems)
                    .ThenInclude(soi => soi.InventoryProduct)
                .ToList();
        }

        /// <summary>
        /// Mark open sales order as paid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var now = DateTime.UtcNow;
            var order = _context.SalesOrders.Find(id);

            order.UpdatedOn = now;
            order.IsPaid = true;

            try
            {
                _context.SalesOrders.Update(order);
                _context.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = $"Order {order.Id} closed: Invoice paid in full",
                    Time = now
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = ex.StackTrace,
                    Time = now
                };
            }
        }
    }
}
