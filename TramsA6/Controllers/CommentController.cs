using System;
using System.Collections.Generic;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using TramsA6.DTOS.CommentModels;

namespace TramsA6.Controllers
{
    [Route("api/Comments")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _repository;

        public CommentController(ICommentRepository repository)
        {
            EnsureArg.IsNotNull(repository);
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Comment> GetAllComments()
        {
            return _repository.GetAll();
        }


        [HttpGet("{id}")]
        public IActionResult GetCommentById(Guid id)
        {
            var comment = _repository.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var status = _repository.Delete(id);
            if (!status)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateCommentDTO updateCommentDto)
        {
            if (updateCommentDto == null)
            {
                return BadRequest();
            }

            var comment = _repository.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            //comment.Update(comment.TransportationMean, comment.User, DateTime.Now, updateCommentDto.Text, 0,
            //  updateCommentDto.Rating);

            comment = Mapper.Map(updateCommentDto, comment);

            _repository.Update(comment);
            return NoContent();
        }
    }
}