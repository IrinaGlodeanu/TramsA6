using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.PersistenceFolder;

namespace BusinessLayer
{
    public class CommentRepository : CrudRepository<Comment>, ICommentRepository
    {
        private readonly IDatabaseContext _context;

        public CommentRepository(IDatabaseContext context) : base(context)
        {
            _context = context;
        }

        public IReadOnlyList<Comment> GetUserComments(Guid userId)
        {
            return _context.Comments.Where(c => c.User.Id == userId).ToList();
        }

        public IReadOnlyList<Comment> GetTransportComments(Guid transportationId)
        {
            return _context.Comments.Where(c => c.TransportationMean.Id == transportationId).ToList();
        }

        public IReadOnlyList<Comment> GetLastPeriodTransportComments(int numberOfMinutes, Guid transportationId)
        {
            DateTime maxTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(numberOfMinutes));

            return _context.Comments.Where(t => t.CreationDate > maxTime && t.TransportationMean.Id == transportationId)
                .ToList();
        }
    }
}