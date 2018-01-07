using AutoMapper;
using Domain.Entities;

namespace TramsA6.DTOS.TransportMeanModels.Mappings
{
    public class TransportMeanProfile : Profile
    {
        public TransportMeanProfile()
        {
            CreateMap<CreateTransportMeanDTO, TransportMean>().ReverseMap();
            CreateMap<UpdateTransportMeanDTO, TransportMean>().ReverseMap();
        }
    }
}
