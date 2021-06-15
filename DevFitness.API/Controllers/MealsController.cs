using AutoMapper;
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
        private readonly IMapper _mapper;

        public MealsController(IMealRepository mealRepository, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _mapper = mapper;
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

            var allMealsViewModel = allMeals
                .Select(m => new MealViewModel(m.Id, m.Description, m.Calories, m.Date));

            return Ok(allMealsViewModel);
        }

        // api/user/4/meals/16 HTTP GET
        [HttpGet("{mealId}")]
        public IActionResult Get(int mealId, int userId)
        {
            var meal = _mealRepository.Get(mealId, userId);

            if (meal == null)
                return NotFound("NENHUM ALIMENTO ENCONTRADO!");

            var mealViewModel = _mapper.Map<MealViewModel>(meal);

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
