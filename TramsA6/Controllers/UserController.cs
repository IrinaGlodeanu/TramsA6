using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using TramsA6.DTOS;

namespace TramsA6.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            Ensure.That(repository).IsNotNull();
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _repository.GetAll();
        }

        //[HttpPost]
        //public IActionResult AddUser([FromBody] CreateUserDto createUserDto)
        //{
        //    if (createUserDto == null)
        //        return BadRequest();

        //    var entity = Domain.Entities.User.Create(createUserDto.Name);
        //    _repository.Add(entity);
        //    return CreatedAtRoute("GetUserById", new {id = entity.Id}, entity);
        //}

        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _repository.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var status = _repository.Delete(id);
            if (!status)
                return NotFound();
            return NoContent();
        }

        //[HttpPut("{id}")]
        //public IActionResult Put(Guid id, [FromBody] UpdateUserDto updateUserDto)
        //{
        //    if (updateUserDto == null || !id.Equals(updateUserDto.Id))
        //        return BadRequest();
        //    var user = _repository.GetById(id);
        //    if (user == null)
        //        return NotFound();
        //    user.Update(updateUserDto.Name);
        //    _repository.Update(user);
        //    return NoContent();
        //}
    }
}