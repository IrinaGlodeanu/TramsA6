using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TramsA6.DTOS;

namespace TramsA6.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _repository;

        public UsersController(IUsersRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _repository.GetAllUsers();
        }

        [HttpPost]
        public IActionResult AddUser([FromBody]CreateUserDto createUserDto)
        {
            if (createUserDto == null)
            {
                return BadRequest();
            }

            var entity = Domain.Entities.User.Create(createUserDto.Name);
            _repository.CreateUser(entity);
            return StatusCode(201, entity);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var status = _repository.DeleteUser(id);
            if (! status)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateUserDto updateUserDto)
        {
            if (updateUserDto == null || updateUserDto.Id != id)
            {
                return BadRequest();
            }
            var user = _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Update(updateUserDto.Name);
            _repository.EditUser(user);
            return NoContent();
        }
    }
}
