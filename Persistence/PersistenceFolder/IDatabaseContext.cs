using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.PersistenceFolder
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}