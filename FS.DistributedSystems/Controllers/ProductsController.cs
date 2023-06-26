using FK.Services.Contracts;
using FS.Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FS.DistributedSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IServicesProduct _servicesProduct;

        public ProductsController(IServicesProduct servicesProduct)
        {
            _servicesProduct = servicesProduct;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            IEnumerable<Product> products = await _servicesProduct.GetProducts();
            return Ok(products);
        }
    }
}
