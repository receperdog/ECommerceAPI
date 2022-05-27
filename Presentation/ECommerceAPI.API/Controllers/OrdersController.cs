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
        private readonly IOrderReadRepository orderReadRepository;
        private readonly IOrderWriteRepository orderWriteRepository;

        public OrdersController(IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            this.orderReadRepository = orderReadRepository;
            this.orderWriteRepository = orderWriteRepository;
        }

        //post api/orders
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            orderWriteRepository.addAsync(order);
            return Ok(order);
        }

    }

}
