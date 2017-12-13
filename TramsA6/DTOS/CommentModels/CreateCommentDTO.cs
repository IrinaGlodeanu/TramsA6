using System;

namespace TramsA6.DTOS.CommentModels
{
    public class CreateCommentDTO
    {
        public Guid TransportationMean { get;  set; }
        public Guid User { get;  set; }
        public string Text { get;  set; }
    }
}
