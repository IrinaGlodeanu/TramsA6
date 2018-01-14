using System.ComponentModel.DataAnnotations;

namespace TramsA6.DTOS.AuthModels
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(5)]
        public string Password { get; set; }
    }
}