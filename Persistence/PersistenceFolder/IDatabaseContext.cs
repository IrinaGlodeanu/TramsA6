using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.PersistenceFolder
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<TransportMean> MeansOfTransport { get; set; }
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}