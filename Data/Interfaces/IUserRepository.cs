using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository : ICrudRepository<User>
    {
        User GetUserByEmail(string email);
    }
}