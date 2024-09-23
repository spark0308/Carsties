using Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThirdService.Data;
using ThirdService.Entities;

namespace ThirdService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public CustomersController(ApplicationDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _context.Customers.ToListAsync();

            return Ok(customers);
        }

        [HttpPost("{gender}")]
        public async Task<IActionResult> Post(string gender)
        {
            var customer = new Customer
            {
                Name = "Name",
                Age = 5,
                Gender = gender,
            };

            var customerCreatedMessage = new CustomerCreated
            {
                Name = customer.Name,
                Gender = customer.Gender,
                Age = customer.Age,
            };

            await _publishEndpoint.Publish(customerCreatedMessage);

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
