using AutoMapper;
using Domain.Entities;

namespace TramsA6.DTOS.CommentModels.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CreateCommentDTO, Comment>().ReverseMap();
            CreateMap<UpdateCommentDTO, Comment>().ReverseMap();
        }
    }
}