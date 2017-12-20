using System;
using System.ComponentModel.DataAnnotations;

namespace TramsA6.DTOS.AuthModels
{
    public class LoginDTO
    {
        [EmailAddress]
        public String Email { get; set; }

        [MinLength(5)]
        public String Password { get; set; }
    }
}