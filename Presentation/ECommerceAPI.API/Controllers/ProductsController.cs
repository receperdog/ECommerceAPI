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

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            this.productWriteRepository = productWriteRepository;
            this.productReadRepository = productReadRepository;
        }    

        //get all products
        [HttpGet]
        public async void Get()
        {
            await productWriteRepository.addAsync(new List<Product> {
                new Product {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Description = "Product 1 description",
                    
                },
                new Product {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Description = "Product 2 description",
                   
                },
                new Product {
                   Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Description = "Product 3 description",
                   
                }
            });

            await productWriteRepository.saveAsync();
        }
        
        
    }
}
