using DevFitness.API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFitness.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateUserInputModel inputModel)
        {
            // return BadRequest

            return CreatedAtAction(nameof(Get), new { id = 10 }, inputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateUserInputModel inputModel)
        {
            //return NotFound();
            //return BadRequest();

            return NoContent();
        }
    }
}
