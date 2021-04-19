using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Serialization
{
    public static class OrderMapper
    {
        // Maps an InvoiceVM to a SalesOrder data model
        public static SalesOrder SerializeInvoiceToOrder(InvoiceViewModel order)
        {
            var salesOrderItems = order.LineItems
                .Select(item => new SalesOrderItem
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    InventoryProduct = ProductMapper.SerializeProductViewModel(item.InventoryProduct)
                }).ToList();

            return new SalesOrder
            {
                SalesOrderItems = salesOrderItems,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn
            };
        }


        //Maps a collection of SalesOrders data model to OrderVM
        public static List<OrderViewModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderViewModel
            {
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                SalesOrderItems = SerializeSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList();
        }


        // Maps a collection of SalesOrderItems data model to SalesOrderItemsVM
        private static List<SalesOrderItemViewModel> SerializeSalesOrderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemViewModel
            {
                Id = item.Id,
                Quantity = item.Quantity,
                InventoryProduct = ProductMapper.SerializeProductViewModel(item.InventoryProduct)
            }).ToList();
        }
    }
}
