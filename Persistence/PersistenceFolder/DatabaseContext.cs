using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.PersistenceFolder
{
    public sealed class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<TransportMean> MeansOfTransport { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TransportMean>().OwnsOne(c => c.Location);
        }
    }
}