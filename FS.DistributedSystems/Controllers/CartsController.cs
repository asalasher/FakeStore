using FK.Services.Contracts;
using FS.Domain.Entities.Contracts;
using FS.Domain.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
//using ILogger = Serilog.ILogger;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FS.DistributedSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IServicesCart _servicesCart;
        private readonly IRepositoryCarts _repositoryCart;
        private readonly ILogger<CartsController> _logger;

        public CartsController(IServicesCart servicesCart, IRepositoryCarts repositoryCart, ILogger<CartsController> logger)
        {
            _servicesCart = servicesCart;
            _repositoryCart = repositoryCart;
            _logger = logger;
        }

        // GET api/<CartsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> Get(int id)
        {
            return Ok(new { id });
        }

        // POST api/<CartsController>
        [HttpPost]
        public async Task<ActionResult<Cart>> Post()
        {
            try
            {
                var newCart = _servicesCart.CreateNewCart();
                return Ok(newCart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error when handling your request");
            }
        }

        // PUT api/<CartsController>/5
        [HttpPut("{idCart}/{idProduct}")]
        public async Task<ActionResult> Put(int idCart, int idProduct)
        {
            try
            {
                Cart? updatedCart = await _servicesCart.AddProductToCart(idCart, idProduct);

                if (updatedCart is not null)
                {
                    return Ok(updatedCart);
                }
                return BadRequest("Unable to carry out request with given information");
            }
            catch (Exception ex)
            {
                // TODO -> _logger.LogError(ex.message);
                return BadRequest("Error when handling your request");
            }
        }

        // DELETE api/<CartsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var status = await _repositoryCart.DeleteAsync(id);
                if (status)
                {
                    return Ok();
                }
                return BadRequest("Your request couldnt be fulfilled");
            }
            catch (Exception ex)
            {
                // TODO -> _logger.LogError(ex.message);
                return BadRequest("We had an issue when processing your request");
            }
        }

    }
}
