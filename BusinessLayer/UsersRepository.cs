using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.PersistenceFolder;

namespace BusinessLayer
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public UsersRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IReadOnlyList<User> GetAllUsers()
        {
            return _databaseContext.Users.ToList();
        }

        public User GetUserById(Guid id)
        {
            return _databaseContext.Users.FirstOrDefault(p => p.Id == id);
        }

        public void CreateUser(User user)
        {
            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();
        }

        public void EditUser(User user)
        {
            _databaseContext.Users.Update(user);
            _databaseContext.SaveChanges();
        }

        public bool DeleteUser(Guid id)
        {
            var user = GetUserById(id);
            if (user == null)
            {
                return false;
            }
            _databaseContext.Users.Remove(user);
            _databaseContext.SaveChanges();
            return true;
        }
    }
}