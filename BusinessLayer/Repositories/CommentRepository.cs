using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.PersistenceFolder;

namespace BusinessLayer.Repositories
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
            return _context.Comments.Where(c => c.UserId == userId).ToList();
        }

        public IReadOnlyList<Comment> GetTransportComments(Guid transportationId)
        {
            return _context.Comments.Where(c => c.TransportMeanId == transportationId).ToList();
        }

        public IReadOnlyList<Comment> GetLastPeriodTransportComments(int numberOfMinutes, Guid transportationId)
        {
            var maxTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(numberOfMinutes));

            return _context.Comments.Where(t => t.CreationDate > maxTime && t.TransportMeanId == transportationId)
                .ToList();
        }
    }
}