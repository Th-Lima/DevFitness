using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;

namespace DevFitness.API.Repositories.Contracts
{
    public interface IUserRepository
    {
        public User Get(int id);

        public void Create(User user);

        public void Update(User user, UpdateUserInputModel inputModel);
    }
}
