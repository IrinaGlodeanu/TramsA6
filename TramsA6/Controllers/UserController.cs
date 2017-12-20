using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using TramsA6.DTOS.CommentModels;
using TramsA6.DTOS.UserModels;

namespace TramsA6.Controllers
{
    [Route("api/Users")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly ITransportMeanRepository _transportMean;

        public UserController(IUserRepository repository, ITransportMeanRepository transportMean)
        {
            Ensure.That(repository).IsNotNull();
            _repository = repository;
            _transportMean = transportMean;
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _repository.GetAll();
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult GetUserById(Guid id)
        {
            Ensure.That(id).IsNotEmpty();
            var user = _repository.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Ensure.That(id).IsNotEmpty();
            var status = _repository.Delete(id);
            if (!status)
                return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            Ensure.That(id).IsNotEmpty();
            if (updateUserDto == null)
                return BadRequest();
            var user = _repository.GetById(id);
            if (user == null)
                return NotFound();
            user.Update(updateUserDto.Name, updateUserDto.Password, updateUserDto.Username, updateUserDto.Email, 0,
                new List<Comment>());
            _repository.Update(user);
            return NoContent();
        }

        [HttpPut("{idUser}/comment")]
        public IActionResult AddCommentToUser(Guid idUser, [FromBody] CreateCommentDTO comment)
        {
            if (idUser.Equals(Guid.Empty))
                return BadRequest();

            if (comment == null)
                return BadRequest();

            var user = _repository.GetById(idUser);

            if (user == null)
                return NotFound();

            //todo: automapper
            Comment com = Comment.Create(_transportMean.GetById(comment.TransportationMean), user, DateTime.Now,
                comment.Text, 0, 0);
            user.Comments.Append(com);

            return NoContent();
        }
    }
}