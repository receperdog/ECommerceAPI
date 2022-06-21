using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.ViewModels.Orders;
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
        public async Task<IActionResult> Post(ViewModel_Order_Create model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            await OrderWriteRepository.addAsync(
                new()
                {
                    CustomerMessage = model.CustomerMessage,
                    Description = model.Description,
                    Address = model.Address
                }
                );
            await OrderWriteRepository.saveAsync();
            return Ok();
        }
        //put Order
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ViewModel_Order_Update model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var Order = await OrderReadRepository.GetByIdAsync(model.Id);
            if (Order == null)
            {
                return NotFound();
            }
            Order.CustomerMessage = model.CustomerMessage;
            Order.Description = model.Description;
            Order.Address = model.Address;
            await OrderWriteRepository.saveAsync();
            return Ok();
        }
    }
}
