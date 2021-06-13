using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Persistence;
using DevFitness.API.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace DevFitness.API.Repositories
{
    public class MealRepository : IMealRepository
    {
        private DevFitnessDbContext _dbContext;

        public MealRepository(DevFitnessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Meal meal)
        {
            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();
        }

        public Meal Get(int mealId, int userId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(x => x.Id == mealId && x.UserId == userId);

            return meal;
        }

        public IList<Meal> GetAll(int userId)
        {
            var allMeals = _dbContext.Meals.Where(x => x.UserId == userId && x.Active).ToList();

            return allMeals;
        }

        public void Update(Meal meal, UpdateMealInputModel inputModel)
        {
            meal.Update(inputModel.Description, inputModel.Calories, inputModel.Date);

            _dbContext.SaveChanges();
        }

        public void Delete(Meal meal)
        {
            // Logic deletion
            meal.Deactivate();

            _dbContext.SaveChanges();
        }
    }
}
