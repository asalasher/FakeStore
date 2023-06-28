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
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IServicesProduct servicesProduct, ILogger<ProductsController> logger)
        {
            _servicesProduct = servicesProduct;
            _logger = logger;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            try
            {
                IEnumerable<Product> products = await _servicesProduct.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("your request could not be fulfilled");
            }
        }
    }
}
