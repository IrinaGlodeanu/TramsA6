using System;
using System.Collections.Generic;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using TramsA6.DTOS;
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
            EnsureArg.IsNotNull(repository);
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
            EnsureArg.IsNotEmpty(id);
            var user = _repository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            EnsureArg.IsNotNull(id);
            var status = _repository.Delete(id);
            if (!status)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateUserDto updateUserDto)
        {
            EnsureArg.IsNotNull(id);
            if (updateUserDto == null)
            {
                return BadRequest();
            }

            var user = _repository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            user = Mapper.Map(updateUserDto, user);

            _repository.Update(user);
            return NoContent();
        }

        [HttpPut("{idUser}/trust")]
        public IActionResult SetTrustForUser(Guid idUser, [FromBody] EditTrustDto editTrustDto)
        {
            EnsureArg.IsNotNull(idUser);
            if (editTrustDto == null)
            {
                return BadRequest();
            }

            var user = _repository.GetById(idUser);
            if (user == null)
            {
                return NotFound();
            }

            user.EditTrust(editTrustDto.Trust);

            _repository.Update(user);
            return NoContent();
        }
    }
}