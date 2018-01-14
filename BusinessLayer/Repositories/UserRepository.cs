using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public override IEnumerable<User> GetAll()
        {
            return _context.Set<User>().Include("Comments").AsNoTracking().ToList();
        }

        public override User GetById(Guid id)
        {
            //Lazy loading is missing from entity framework core
            //Explicit loading
            return _context.Set<User>().Include("Comments").AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(
                c => string.Equals(c.Email, email, StringComparison.OrdinalIgnoreCase));
        }
    }
}