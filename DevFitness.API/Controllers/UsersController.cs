using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DevFitness.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userRepository.Get(id);

            if (user == null)
            {
                return NotFound("NENHUM USUÁRIO ENCONTRADO!");
            }

            var userViewModel = new UserViewModel(user.Id, user.FullName, user.Height, user.Weight, user.BirthDate);

            return Ok(userViewModel);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUserInputModel inputModel)
        {
            var user = new User(inputModel.FullName, inputModel.Height, inputModel.Weight, inputModel.BirthDate);

            _userRepository.Create(user);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, inputModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserInputModel inputModel)
        {
            var user = _userRepository.Get(id);

            if (user == null)
            {
                return NotFound("NENHUM USUÁRIO ENCONTRADO!");
            }

            if(inputModel.Height < user.Height)
            {
                return BadRequest("NÃO É POSSÍVEL INSERIR UMA ALTURA MENOR DO QUE O ATUAL");
            }

            _userRepository.Update(user, inputModel);

            return NoContent();
        }
    }
}
