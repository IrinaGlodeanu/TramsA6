using System;
using System.Linq;
using BusinessLayer.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.PersistenceFolder;

namespace BusinessLayer
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        private readonly IDatabaseContext _context;

        public UserRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
            _context = databaseContext;

        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(
                c => String.Equals(c.Email, email, StringComparison.OrdinalIgnoreCase));
        }
    }
}