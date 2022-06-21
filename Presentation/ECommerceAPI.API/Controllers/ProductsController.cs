using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.ViewModels.Products;
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
        public async Task<IActionResult> PostProduct(ViewModel_Product_Create model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            
            await _productWriteRepository.addAsync(
                new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price
                }
                );
            await _productWriteRepository.saveAsync();
            return Ok();
            
        }
        //put product
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(ViewModel_Product_Update model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var product = await _productReadRepository.GetByIdAsync(model.Id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.Quantity = model.Quantity;
            await _productWriteRepository.saveAsync();
            return Ok();
        }
        //delete product
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productWriteRepository.deleteByIdAsync(id);
            await _productWriteRepository.saveAsync();
            return Ok();
        }
    }
}
