using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Order;
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
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService,
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
            _orderService = orderService;
        }

        [HttpPost]
        public ActionResult GenerateNewOrder([FromBody] InvoiceViewModel invoice)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation("Generating Invoice");
            var order = OrderMapper.SerializeInvoiceToOrder(invoice);
            order.Customer = _customerService.GetById(invoice.CustomerId);
            _orderService.GenerateOpenOrder(order);
            return Ok();
        }

        [HttpGet]
        public ActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();
            var orderViewModel = OrderMapper.SerializeOrdersToViewModels(orders);

            return Ok(orderViewModel);
        }

        [HttpPatch("complete/{id}")]
        public ActionResult MarkOrderComplete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation($"Marking order {id} complete");
            _orderService.MarkFulfilled(id);

            return Ok();
        }
    }
}
