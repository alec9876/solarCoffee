using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Customer;
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
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetCustomers()
        {
            _logger.LogInformation("Getting Customer");
            var customers = _customerService.GetAllCustomers();
            var customerViewModel = customers.Select(c => CustomerMapper.SerializeCustomer(c));

            return Ok(customerViewModel);
        }

        [HttpGet("{id}")]
        public ActionResult GetCustomer(int id)
        {
            _logger.LogInformation("Getting a customer");
            var customer = _customerService.GetById(id);

            return Ok(customer);
        }

        [HttpPost]
        public ActionResult CreateCustomer([FromBody] CustomerViewModel customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _logger.LogInformation("Creating a new customer");
            customer.CreatedOn = DateTime.UtcNow;
            customer.UpdatedOn = DateTime.UtcNow;
            var customerData = CustomerMapper.SerializeCustomer(customer);
            var newCustomer = _customerService.CreateCustomer(customerData);
            return Ok(newCustomer);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Delete Customer");
            var response = _customerService.DeleteCustomer(id);
            return Ok(response);
        }
    }
}
