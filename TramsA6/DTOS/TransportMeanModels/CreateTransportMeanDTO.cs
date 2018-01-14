using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.ValueObjects;

namespace TramsA6.DTOS.TransportMeanModels
{
    public class CreateTransportMeanDTO
    {
        public string IdentifyingCode { get; set; }

        [Range(1, 5)]
        [DefaultValue(0)]
        public double Rating { get; set; }

        public int LineNumber { get; set; }

        public Coordinates Location { get; set; }
    }
}