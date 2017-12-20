using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using TramsA6.DTOS.CommentModels;
using TramsA6.DTOS.TransportMeanModels;

namespace TramsA6.Controllers
{
    [Route("api/MeansOfTransport")]
    public class TransportMeanController : Controller
    {
        private readonly ITransportMeanRepository _repository;
        private readonly IUserRepository _userRepository;

        public TransportMeanController(ITransportMeanRepository repository, IUserRepository userRepository)
        {
            Ensure.That(repository).IsNotNull();
            Ensure.That(userRepository).IsNotNull();
            _repository = repository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<TransportMean> GetAllMeansOfTransport()
        {
            return _repository.GetAll();
        }

        [HttpPost]
        public IActionResult AddMeanOfTransport([FromBody] CreateTransportMeanDTO createTransportMeanDTO)
        {
            if (createTransportMeanDTO == null)
                return BadRequest();

            var meanOfTransport = Domain.Entities.TransportMean.Create(createTransportMeanDTO.IdentifyingCode,
                new List<Comment>(), createTransportMeanDTO.Rating, createTransportMeanDTO.Location);
            _repository.Add(meanOfTransport);
            return CreatedAtRoute("GetMeanOfTransportById", new {id = meanOfTransport.Id}, meanOfTransport);
        }

        [HttpGet("{id}", Name = "GetMeanOfTransportById")]
        public IActionResult GetMeanOfTransportById(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return BadRequest();
            var meanOfTransport = _repository.GetById(id);
            if (meanOfTransport == null)
                return NotFound();
            return Ok(meanOfTransport);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id.Equals(Guid.Empty))
                return BadRequest();
            var status = _repository.Delete(id);
            if (!status)
                return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateTransportMeanDTO updateTransportMeanDto)
        {
            if (id.Equals(Guid.Empty))
                return BadRequest();
            if (updateTransportMeanDto == null)
                return BadRequest();
            var meanOfTransport = _repository.GetById(id);
            if (meanOfTransport == null)
                return NotFound();
            meanOfTransport.Update(meanOfTransport.IdentifyingCode, new List<Comment>(), updateTransportMeanDto.Rating,
                updateTransportMeanDto.Location);
            _repository.Update(meanOfTransport);
            return NoContent();
        }


        [HttpPut("{idMeanOfTransport}/comment")]
        public IActionResult AddCommentToMeanOfTransport(Guid idMeanOfTransport, [FromBody] CreateCommentDTO comment)
        {
            if (idMeanOfTransport.Equals(Guid.Empty))
                return BadRequest();
            if (comment == null)
                return BadRequest();
            var meanOfTransport = _repository.GetById(idMeanOfTransport);
            if (meanOfTransport == null)
                return NotFound();
            //todo: automapper
            Comment com = Comment.Create(meanOfTransport, _userRepository.GetById(comment.User), DateTime.Now,
                comment.Text, 0, 0);

            meanOfTransport.Comments.Append(com);
            return NoContent();
        }
    }
}