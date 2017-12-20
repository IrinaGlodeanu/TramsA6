using System.ComponentModel.DataAnnotations;

namespace TramsA6.DTOS.UserModels
{
    public class CreateUserDto
    {
        [MaxLength(30)]
        public string Name { get; set; }

        [MinLength(5)]
        public string Password { get; set; }

        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}