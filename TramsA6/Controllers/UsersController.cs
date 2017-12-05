using System;
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
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _repository.GetAllUsers();
        }

        [HttpPost]
        public IActionResult AddUser([FromBody]UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var entity = Domain.Entities.User.Create(userDto.Name);
            _repository.CreateUser(entity);
            return Ok(entity);
        }

        [HttpGet("{id}")]
        public IActionResult GetByUserId(Guid id)
        {
            var user = _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _repository.DeleteUser(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]UserDto userDto)
        {
            var user = _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Update(userDto.Name);
            _repository.EditUser(user);
            return Ok(user);
        }



    }
}
