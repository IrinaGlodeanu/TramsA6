using AutoMapper;
using Domain.Entities;

namespace TramsA6.DTOS.UserModels.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<UserCreationResponseDTO, User>().ReverseMap();
        }
    }
}