using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Web.Serialization
{
    public static class ProductMapper
    {
        public static ProductViewModel SerializeProductViewModel(Product product)
        {
            // Maps a product data model to a product view model
            return new ProductViewModel
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }

        public static Product SerializeProductViewModel(ProductViewModel product)
        {
            // Maps a product view model to a product data model
            return new Product
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
    }
}
