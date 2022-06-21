using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly private IOrderWriteRepository OrderWriteRepository;
        readonly private IOrderReadRepository OrderReadRepository;

        public OrdersController(IOrderWriteRepository OrderWriteRepository, IOrderReadRepository OrderReadRepository)
        {
            this.OrderWriteRepository = OrderWriteRepository;
            this.OrderReadRepository = OrderReadRepository;
        }

        //get all Order
        [HttpGet]
        public IActionResult Get()
        {
            var Orders = OrderReadRepository.GetAll();
            return Ok(Orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var Order = await OrderReadRepository.GetByIdAsync(id);
            if (Order == null)
            {
                return NotFound();
            }
            return Ok(Order);
        }

        //post Order
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order Order)
        {
            if (Order == null)
            {
                return BadRequest();
            }
            await OrderWriteRepository.addAsync(Order);
            return CreatedAtAction("Get", new { id = Order.Id }, Order);
        }
        //put Order
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Order Order)
        {
            if (Order == null)
            {
                return BadRequest();
            }
            var OrderToUpdate = await OrderReadRepository.GetByIdAsync(id);
            if (OrderToUpdate == null)
            {
                return NotFound();
            }
            OrderToUpdate.Address = Order.Address;
            OrderToUpdate.CustomerMessage = Order.CustomerMessage;
            OrderToUpdate.Description = Order.Description;
            await OrderWriteRepository.saveAsync();
            return Ok();



        }
    }
}
