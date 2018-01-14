using System;

namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        private Comment()
        {
        }

        public Guid TransportMeanId { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Text { get; private set; }
        public double Trust { get; private set; }
        public double Rating { get; private set; }


        public static Comment Create(Guid transportationMeanId, Guid userId, DateTime creationDate, string text,
            double trust, double rating)
        {
            var newComment = new Comment
            {
                TransportMeanId = transportationMeanId,
                UserId = userId,
                CreationDate = creationDate,
                Text = text,
                Trust = trust,
                Rating = rating
            };

            return newComment;
        }

        public void Update(Guid transportationMeanId, Guid userId, DateTime creationDate, string text,
            double trust, double rating)
        {
            TransportMeanId = transportationMeanId;
            UserId = userId;
            CreationDate = creationDate;
            Text = text;
            Trust = trust;
            Rating = rating;
        }
    }
}