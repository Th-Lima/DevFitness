using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevFitness.API.Controllers
{
    [Route("api/users/{userId}/meals")]
    public class MealsController : ControllerBase
    {
        private readonly IMealRepository _mealRepository;

        public MealsController(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        // api/user/4/meals HTTP GET
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            var allMeals = _mealRepository.GetAll(userId);

            if(!allMeals.Any())
            {
                return NotFound("NENHUM ALIMENTO ENCONTRADO!");
            }

            var allMealsViewModels = allMeals
                .Select(m => new MealViewModel(m.Id, m.Description, m.Calories, m.Date));

            return Ok(allMealsViewModels);
        }

        // api/user/4/meals/16 HTTP GET
        [HttpGet("{mealId}")]
        public IActionResult Get(int mealId, int userId)
        {
            var meal = _mealRepository.Get(mealId, userId);

            if (meal == null)
                return NotFound("NENHUM ALIMENTO ENCONTRADO!");

            var mealViewModel = new MealViewModel(meal.Id, meal.Description, meal.Calories, meal.Date);

            return Ok(mealViewModel);
        }

        // api/user/4/meals HTTP POST
        [HttpPost]
        public IActionResult Post(int userId, [FromBody] CreateMealInputModel inputModel)
        {
            var meal = new Meal(inputModel.Description, inputModel.Calories, inputModel.Date, userId);

            _mealRepository.Create(meal);

            return CreatedAtAction(nameof(Get), new
            {
                userId = userId,
                mealId = meal.Id,
            }, inputModel);
        }

        // api/user/4/meals/16 HTTP PUT
        [HttpPut("{mealId}")]
        public IActionResult Put(int mealId, int userId, [FromBody] UpdateMealInputModel inputModel)
        {
            var meal = _mealRepository.Get(mealId, userId);

            if(meal == null)
            {
                return NotFound("NENHUM ALIMENTO ENCONTRADO!");
            }

            _mealRepository.Update(meal, inputModel);

            return NoContent();
        }

        // api/user/4/meals/16 HTTP DELETE
        [HttpDelete("{mealId}")]
        public IActionResult Delete(int mealId, int userId)
        {
            var meal = _mealRepository.Get(mealId, userId);

            if (meal == null)
            {
                return NotFound("NENHUM ALIMENTO ENCONTRADO!");
            }

            _mealRepository.Delete(meal);

            return NoContent();
        }
    }
}
