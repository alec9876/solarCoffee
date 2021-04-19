using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Inventory;
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
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _inventoryService;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public ActionResult GetInventories()
        {
            _logger.LogInformation("Getting inventories");
            var inventory = _inventoryService.GetCurrentInventory();
            var inventoryViewModel = inventory
                .Select(i => InventoryMapper.SerializeInventory(i))
                .OrderBy(i => i.InventoryProduct.Name)
                .ToList();

            return Ok(inventoryViewModel);
        }

        [HttpPatch]
        public ActionResult UpdateInventory([FromBody] ShipmentViewModel shipment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation($"Updating inventory for {shipment.ProductId} - Adjustment {shipment.Adjustment}");

            var id = shipment.ProductId;
            var adjustment = shipment.Adjustment;
            var inventory = _inventoryService.UpdateUnitsAvailable(id, adjustment);
            return Ok(inventory);
        }
    }
}
