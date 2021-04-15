using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolarCoffee.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDBContext _context;
        public ProductService(SolarDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Archives a product by setting boolean archive to true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                product.IsArchived = true;
                _context.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Product archived",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = $"Error archiving product: {ex.StackTrace}",
                    IsSuccess = true
                };
            }
        }

        /// <summary>
        /// Adds new product to DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ServiceResponse<Data.Models.Product> CreatedProduct(Data.Models.Product product)
        {
            try
            {
                _context.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _context.ProductInventories.Add(newInventory);

                _context.SaveChanges();

                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Data.Models.Product>
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = $"Error saving new product: {ex.Message}",
                    IsSuccess = false
                };
            }
            
        }

        /// <summary>
        /// Gets all products in db
        /// </summary>
        /// <returns></returns>
        public List<Data.Models.Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        /// <summary>
        /// Gets product by product id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Models.Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }
    }
}
