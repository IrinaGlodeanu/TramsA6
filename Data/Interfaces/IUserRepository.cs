using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        IReadOnlyList<User> GetAllUsers();
        User GetUserById(Guid id);
        void CreateUser(User user);
        void EditUser(User user);
        bool DeleteUser(Guid id);
    }
}