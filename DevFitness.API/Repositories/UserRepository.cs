using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Persistence;
using DevFitness.API.Repositories.Contracts;
using System.Linq;

namespace DevFitness.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DevFitnessDbContext _dbContext;

        public UserRepository(DevFitnessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(User user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }

        public User Get(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == id && x.Active);

            return user;
        }

        public void Update(User user, UpdateUserInputModel inputModel)
        {
            user.Update(inputModel.Height, inputModel.Weight);

            _dbContext.SaveChanges();
        }
    }
}
