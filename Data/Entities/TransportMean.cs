using System.Collections.Generic;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class TransportMean : BaseEntity
    {
        private TransportMean()
        {
        }

        public string IdentifyingCode { get; private set; }
        public IEnumerable<Comment> Comments { get; private set; }
        public double Rating { get; private set; }
        public Coordinates Location { get; private set; }


        public static TransportMean Create(string identifyingCode, List<Comment> comments, double rating,
            Coordinates location)
        {
            var newTransportMean = new TransportMean
            {
                IdentifyingCode = identifyingCode,
                Comments = comments,
                Rating = rating,
                Location = location
            };

            return newTransportMean;
        }

        public void Update(string identifyingCode, List<Comment> comments, double rating, Coordinates location)
        {
            IdentifyingCode = identifyingCode;
            Comments = comments;
            Rating = rating;
            Location = location;
        }
    }
}