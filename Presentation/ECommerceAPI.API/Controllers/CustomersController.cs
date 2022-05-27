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
        readonly private ICustomerReadRepository customerReadRepository;
        readonly private ICustomerWriteRepository customerWriteRepository;

        public CustomersController(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            this.customerReadRepository = customerReadRepository;
            this.customerWriteRepository = customerWriteRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = customerReadRepository.GetAll();
            return Ok(customers);
        }

        //post customer
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            customerWriteRepository.addAsync(customer);
            return Ok();
        }
    }
}
