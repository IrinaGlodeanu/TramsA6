using System;
using System.Collections.Generic;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using TramsA6.DTOS;
using TramsA6.DTOS.CommentModels;
using TramsA6.DTOS.TransportMeanModels;

namespace TramsA6.Controllers
{
    [Route("api/MeansOfTransport")]
    public class TransportMeanController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITransportMeanRepository _repository;


        public TransportMeanController(ITransportMeanRepository repository, ICommentRepository commentRepository)
        {
            EnsureArg.IsNotNull(repository);
            EnsureArg.IsNotNull(commentRepository);
            _repository = repository;
            _commentRepository = commentRepository;
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
            {
                return BadRequest();
            }

            var meanOfTransport = TransportMean.Create(createTransportMeanDTO.IdentifyingCode,
                new List<Comment>(), createTransportMeanDTO.Rating, createTransportMeanDTO.Location,
                createTransportMeanDTO.LineNumber);


            _repository.Add(meanOfTransport);
            return CreatedAtRoute("GetMeanOfTransportById", new {id = meanOfTransport.Id}, meanOfTransport);
        }

        [HttpGet("{id}", Name = "GetMeanOfTransportById")]
        public IActionResult GetMeanOfTransportById(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return BadRequest();
            }

            var meanOfTransport = _repository.GetById(id);
            if (meanOfTransport == null)
            {
                return NotFound();
            }
            return Ok(meanOfTransport);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return BadRequest();
            }

            var status = _repository.Delete(id);
            if (!status)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateTransportMeanDTO updateTransportMeanDto)
        {
            if (id.Equals(Guid.Empty))
            {
                return BadRequest();
            }
            if (updateTransportMeanDto == null)
            {
                return BadRequest();
            }
            var meanOfTransport = _repository.GetById(id);
            if (meanOfTransport == null)
            {
                return NotFound();
            }

            meanOfTransport = Mapper.Map(updateTransportMeanDto, meanOfTransport);

            _repository.Update(meanOfTransport);
            return NoContent();
        }


        [HttpPut("{idMeanOfTransport}/comment")]
        public IActionResult AddCommentToMeanOfTransport(Guid idMeanOfTransport, [FromBody] CreateCommentDTO comment)
        {
            if (idMeanOfTransport.Equals(Guid.Empty))
            {
                return BadRequest();
            }

            if (comment == null)
            {
                return BadRequest();
            }

            var meanOfTransport = _repository.GetById(idMeanOfTransport);
            if (meanOfTransport == null)
            {
                return NotFound();
            }

            var com = Comment.Create(meanOfTransport.Id, comment.UserId, DateTime.Now,
                comment.Text, 0, 0);

            _commentRepository.Add(com);

            return NoContent();
        }

        [HttpGet("/line/{numberOfLine}")]
        public IActionResult GetMeanOfTransportByLine(int numberOfLine)
        {
            if (numberOfLine == 0)
            {
                BadRequest();
            }

            var meanOfTransport = _repository.GetMeansOTransportByLineNumber(numberOfLine);
            if (meanOfTransport == null)
            {
                return NotFound();
            }

            return Ok(meanOfTransport);
        }

        [HttpPut("{idMeanOfTransport}/rating")]
        public IActionResult SetRatingForMeanOfTransport(Guid idMeanOfTransport, [FromBody] EditTrustDto editTrustDto)
        {
            EnsureArg.IsNotNull(idMeanOfTransport);
            if (editTrustDto == null)
            {
                return BadRequest();
            }

            var user = _repository.GetById(idMeanOfTransport);
            if (user == null)
            {
                return NotFound();
            }

            user.EditRating(editTrustDto.Trust);

            _repository.Update(user);
            return NoContent();
        }
    }
}