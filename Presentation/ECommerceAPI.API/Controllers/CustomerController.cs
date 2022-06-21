using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.ViewModels.Customers;
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
        public async Task<IActionResult> Post(ViewModel_Customer_Create model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            await CustomerWriteRepository.addAsync(
                new()
                {                    
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone                        
                }
                );
            await CustomerWriteRepository.saveAsync();
            return Ok();
        }
        //put Customer
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ViewModel_Customer_Update model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var Customer = await CustomerReadRepository.GetByIdAsync(model.Id);
            if (Customer == null)
            {
                return NotFound();
            }
            Customer.FirstName = model.FirstName;
            Customer.LastName = model.LastName;
            Customer.Email = model.Email;
            Customer.Phone = model.Phone;
            await CustomerWriteRepository.saveAsync();
            return Ok();

        }
        
        
    }
}
