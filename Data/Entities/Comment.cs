using System;

namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        private Comment()
        {
        }

        public TransportMean TransportationMean { get; private set; }
        public User User { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Text { get; private set; }
        public double Trust { get; private set; }
        public double Rating { get; private set; }


        public static Comment Create(TransportMean transportationMean, User user, DateTime creationDate, string text,
            double trust, double rating)
        {
            var newComment = new Comment
            {
                TransportationMean = transportationMean,
                User = user,
                CreationDate = creationDate,
                Text = text,
                Trust = trust,
                Rating = rating
            };

            return newComment;
        }

        public void Update(TransportMean transportationMean, User user, DateTime creationDate, string text,
            double trust, double rating)
        {
            TransportationMean = transportationMean;
            User = user;
            CreationDate = creationDate;
            Text = text;
            Trust = trust;
            Rating = rating;
        }
    }
}