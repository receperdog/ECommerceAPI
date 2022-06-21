using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository productWriteRepository;
        readonly private IProductReadRepository productReadRepository;
        readonly private IOrderWriteRepository orderWriteRepository;
        readonly private ICustomerWriteRepository customerWriteRepository;
        

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository)
        {
            this.productWriteRepository = productWriteRepository;
            this.productReadRepository = productReadRepository;
            this.orderWriteRepository = orderWriteRepository;
            this.customerWriteRepository = customerWriteRepository;
        }    

        [HttpGet]
        public async Task Get()
        {
            var customerId = Guid.NewGuid();
            await customerWriteRepository.addAsync(new() { Id = customerId, FirstName = "John", LastName = "Doe" });
            await orderWriteRepository.addAsync(new() { Description = "Teste", Address = "ankara", CustomerId = customerId });
            await orderWriteRepository.addAsync(new() { Description = "Meste", Address = "antalya" });
            await orderWriteRepository.saveAsync();
            await customerWriteRepository.saveAsync();


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var product = await productReadRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


    }
}
