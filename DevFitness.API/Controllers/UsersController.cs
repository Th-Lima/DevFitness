using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFitness.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userRepository.Get(id);

            if (user == null)
            {
                return NotFound("NENHUM USUÁRIO ENCONTRADO!");
            }

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return Ok(userViewModel);
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="inputModel">Object with data to register user</param>
        /// <remarks>
        /// Example Request:
        /// {
        /// "fullName" : "Test full name",
        /// "height": 1.70,
        /// "weigth": 70,
        /// "birthDate": "1990-01-01 00:00:00"
        /// }
        /// </remarks>
        /// <returns>Object created</returns>
        /// <response code="201">Object created successfully</response>
        /// <response code="400">Invalid data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] CreateUserInputModel inputModel)
        {
            var user = _mapper.Map<User>(inputModel);

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
