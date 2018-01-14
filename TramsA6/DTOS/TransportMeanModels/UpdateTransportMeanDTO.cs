using Domain.ValueObjects;

namespace TramsA6.DTOS.TransportMeanModels
{
    public class UpdateTransportMeanDTO
    {
        public int LineNumber { get; set; }

        public Coordinates Location { get; set; }
    }
}