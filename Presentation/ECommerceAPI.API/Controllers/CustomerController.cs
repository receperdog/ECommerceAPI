using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        readonly private ICustomerWriteRepository CustomerWriteRepository;
        readonly private ICustomerReadRepository CustomerReadRepository;

        public CustomersController(ICustomerWriteRepository CustomerWriteRepository, ICustomerReadRepository CustomerReadRepository)
        {
            this.CustomerWriteRepository = CustomerWriteRepository;
            this.CustomerReadRepository = CustomerReadRepository;
        }

        //get all Customer
        [HttpGet]
        public IActionResult Get()
        {
            var Customers = CustomerReadRepository.GetAll();
            return Ok(Customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var Customer = await CustomerReadRepository.GetByIdAsync(id);
            if (Customer == null)
            {
                return NotFound();
            }
            return Ok(Customer);
        }

        //post Customer
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer Customer)
        {
            if (Customer == null)
            {
                return BadRequest();
            }
            await CustomerWriteRepository.addAsync(Customer);
            return CreatedAtAction("Get", new { id = Customer.Id }, Customer);
        }
        //put Customer
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Customer Customer)
        {
            if (Customer == null)
            {
                return BadRequest();
            }
            var CustomerToUpdate = await CustomerReadRepository.GetByIdAsync(id);
            if (CustomerToUpdate == null)
            {
                return NotFound();
            }
            CustomerToUpdate.FirstName = Customer.FirstName;
            CustomerToUpdate.LastName = Customer.LastName;
            CustomerToUpdate.Email = Customer.Email;
            CustomerToUpdate.Phone = Customer.Phone;

            await CustomerWriteRepository.saveAsync();
            return Ok();



        }
    }
}
