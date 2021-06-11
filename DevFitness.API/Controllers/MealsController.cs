using DevFitness.API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFitness.API.Controllers
{
    [Route("api/users/{userId}/meals")]
    public class MealsController : ControllerBase
    {
        // api/user/4/meals HTTP GET
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            return Ok();
        }

        // api/user/4/meals/16 HTTP GET
        [HttpGet("{mealId}")]
        public IActionResult Get(int userId, int mealId)
        {
            return Ok();
        }

        // api/user/4/meals HTTP POST
        [HttpPost]
        public IActionResult Post(int userId, [FromBody]CreateMealInputModel inputModel)
        {
            return Ok();
        }

        // api/user/4/meals/16 HTTP PUT
        [HttpPut("{mealId}")]
        public IActionResult Put(int userId, int mealId,  [FromBody]UpdateMealInputModel inputModel)
        {
            return NoContent();
        }

        // api/user/4/meals/16 HTTP DELETE
        [HttpDelete("{mealId}")]
        public IActionResult Delete(int userId, int mealId)
        {
            return NoContent();
        }
    }
}
