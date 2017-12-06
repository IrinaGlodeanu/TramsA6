using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUsersRepository
    {
        IReadOnlyList<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void EditUser(User user);
        bool DeleteUser(int id);
    }
}