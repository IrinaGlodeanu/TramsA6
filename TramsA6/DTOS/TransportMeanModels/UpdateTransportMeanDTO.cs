using Domain.ValueObjects;

namespace TramsA6.DTOS.TransportMeanModels
{
    public class UpdateTransportMeanDTO
    {
        public double Rating { get;  set; }
        public Coordinates Location { get;  set; }
    }
}
