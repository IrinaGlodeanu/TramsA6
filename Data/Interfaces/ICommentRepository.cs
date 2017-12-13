using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICommentRepository : ICrudRepository<Comment>
    {
        IReadOnlyList<Comment> GetUserComments(Guid userId);
        IReadOnlyList<Comment> GetTransportComments(Guid transportationId);
        IReadOnlyList<Comment> GetLastPeriodTransportComments(int numberOfMinutes, Guid transportationId);
    }
}