using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAuthenticationService
    {
        void Register(User user, string password);
        bool Login(string email, string password);
    }
}