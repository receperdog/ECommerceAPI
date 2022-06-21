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
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        //get all product
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productReadRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //post product
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            await _productWriteRepository.addAsync(product);
            return CreatedAtAction("Get", new { id = product.Id }, product);
        }
        //put product
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            var productToUpdate = await _productReadRepository.GetByIdAsync(id);
            if (productToUpdate == null)
            {
                return NotFound();
            }
            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.Description = product.Description;
            await _productWriteRepository.saveAsync();
            return Ok();



        }
    }
}
