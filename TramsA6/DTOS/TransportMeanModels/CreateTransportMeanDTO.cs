using Domain.ValueObjects;

namespace TramsA6.DTOS.TransportMeanModels
{
    public class CreateTransportMeanDTO
    {
        public string IdentifyingCode { get; set; }
        public double Rating { get; set; }
        public Coordinates Location { get; set; }
    }
}
