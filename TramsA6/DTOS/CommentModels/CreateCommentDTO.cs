using System;
using System.ComponentModel.DataAnnotations;

namespace TramsA6.DTOS.CommentModels
{
    public class CreateCommentDTO
    {
        public Guid TransportMeanId { get; set; }

        public Guid UserId { get; set; }

        [MaxLength(25)]
        public string Text { get; set; }
    }
}