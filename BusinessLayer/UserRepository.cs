using Domain.Entities;
using Domain.Interfaces;
using Persistence.PersistenceFolder;

namespace BusinessLayer
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {

        }

    }
}