using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Serialization
{
    public class InventoryMapper
    {

        //Serializes an inventory data model to a inventoryVM
        public static InventoryViewModel SerializeInventory(ProductInventory inventory)
        {
            return new InventoryViewModel
            {
                Id = inventory.Id,
                CreatedOn = inventory.CreatedOn,
                UpdatedOn = inventory.UpdatedOn,
                QuantityOnHand = inventory.QuantityOnHand,
                IdealQuantity = inventory.IdealQuantity,
                InventoryProduct = ProductMapper.SerializeProductViewModel(inventory.InventoryProduct)
            };
        }

        //Serializes an inventoryVM to an inventory data model
        public static ProductInventory SerializeInventory(InventoryViewModel inventory)
        {
            return new ProductInventory
            {
                Id = inventory.Id,
                CreatedOn = inventory.CreatedOn,
                UpdatedOn = inventory.UpdatedOn,
                QuantityOnHand = inventory.QuantityOnHand,
                IdealQuantity = inventory.IdealQuantity,
                InventoryProduct = ProductMapper.SerializeProductViewModel(inventory.InventoryProduct)
            };
        }
    }
}
