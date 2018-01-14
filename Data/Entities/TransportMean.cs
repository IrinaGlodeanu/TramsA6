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
        public List<Comment> Comments { get; private set; }
        public double Rating { get; private set; }
        public Coordinates Location { get; private set; }
        public int LineNumber { get; private set; }
        public int NumberOfVotes { get; private set; }


        public static TransportMean Create(string identifyingCode, List<Comment> comments, double rating,
            Coordinates location, int lineNumber)
        {
            var newTransportMean = new TransportMean
            {
                IdentifyingCode = identifyingCode,
                Comments = comments,
                Rating = rating,
                Location = location,
                LineNumber = lineNumber
            };

            return newTransportMean;
        }

        public void Update(string identifyingCode, List<Comment> comments, double rating, Coordinates location,
            int lineNumber)
        {
            IdentifyingCode = identifyingCode;
            Comments = comments;
            Rating = rating;
            Location = location;
            LineNumber = lineNumber;
        }

        public void AddCommentToList(Comment com)
        {
            if (Comments == null)
            {
                Comments = new List<Comment>();
            }
            Comments.Add(com);
        }

        public void EditRating(double newValue)
        {
            NumberOfVotes = NumberOfVotes + 1;
            Rating = (Rating + newValue) / NumberOfVotes;
        }
    }
}