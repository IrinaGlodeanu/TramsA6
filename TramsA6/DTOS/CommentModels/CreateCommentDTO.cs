using System;
using System.ComponentModel.DataAnnotations;

namespace TramsA6.DTOS.CommentModels
{
    public class CreateCommentDTO
    {
        public Guid TransportationMean { get; set; }

        public Guid User { get; set; }

        [MaxLength(25)]
        public string Text { get; set; }
    }
}