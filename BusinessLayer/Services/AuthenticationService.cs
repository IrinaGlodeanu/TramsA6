using System;
using System.Security.Cryptography;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BusinessLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public void Register(User user, string password)
        {
            var salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var hashedPassword =
                Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));

            user.SetAuthenticationData(hashedPassword, salt);

            _userRepository.Add(user);
        }


        public bool Login(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
                return false;

            var hashedPassword =
                Convert.ToBase64String(KeyDerivation.Pbkdf2(password, user.Salt, KeyDerivationPrf.HMACSHA1, 10000,
                    256 / 8));

            if (!user.Password.Equals(hashedPassword))
            {
                return false;
            }

            return true;
        }
    }
}