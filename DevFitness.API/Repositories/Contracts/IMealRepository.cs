using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using System.Collections.Generic;

namespace DevFitness.API.Repositories.Contracts
{
    public interface IMealRepository
    {
        //CRUD

        public void Create(Meal meal);

        public IList<Meal> GetAll(int userId);

        public Meal Get(int mealId, int userId);

        public void Update(Meal meal, UpdateMealInputModel inputModel);

        public void Delete(Meal meal);
    }
}
