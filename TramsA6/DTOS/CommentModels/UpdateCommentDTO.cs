using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TramsA6.DTOS.CommentModels
{
    public class UpdateCommentDTO
    {
        [MaxLength(25)]
        public string Text { get;  set; }

        [Range(1,5), DefaultValue(0)]
        public double Rating { get;  set; }
    }
}
