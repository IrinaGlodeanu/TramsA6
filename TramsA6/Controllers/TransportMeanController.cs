using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using TramsA6.DTOS.TransportMeanModels;

namespace TramsA6.Controllers
{
    [Route("api/TransportMeans")]
    public class TransportMeanController : Controller
    {
        private readonly ITransportMeanRepository _repository;

        public TransportMeanController(ITransportMeanRepository repository)
        {
            Ensure.That(repository).IsNotNull();
            _repository = repository;
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
            var meanOfTransport = _repository.GetById(id);
            if (meanOfTransport == null)
                return NotFound();
            return Ok(meanOfTransport);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var status = _repository.Delete(id);
            if (!status)
                return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateTransportMeanDTO updateTransportMeanDto)
        {
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

       //todo: addComment to mean of transport functionality
    }
}
