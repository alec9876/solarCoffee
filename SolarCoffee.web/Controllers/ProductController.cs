using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCoffee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost]
        public ActionResult AddProduct([FromBody] ProductViewModel product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation("Adding product");
            var newProduct = ProductMapper.SerializeProductViewModel(product);
            var response = _productService.CreateProduct(newProduct);
            return Ok(response);
        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            _logger.LogInformation("Getting all products");

            var products = _productService.GetAllProducts();

            var productViewModel = products.Select(
                product => ProductMapper.SerializeProductViewModel(product));

            return Ok(productViewModel);
        }

        [HttpPatch("{id}")]
        public ActionResult ArchiveProduct(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation("Archiving product");
            var archiveResult = _productService.ArchiveProduct(id);
            return Ok(archiveResult);
        }
    }
}
