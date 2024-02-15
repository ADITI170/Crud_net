using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerService.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            var createdCustomer = _customerService.CreateCustomer(customer);
            return CreatedAtAction(nameof(Get), new { id = createdCustomer.CustomerId }, createdCustomer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Customer customer)
        {
            var updatedCustomer = _customerService.UpdateCustomer(customer, id);
            if (updatedCustomer == null)
            {
                return NotFound();
            }
            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}