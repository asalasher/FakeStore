using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FS.DistributedSystems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {

        // GET api/<CartsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CartsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CartsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
