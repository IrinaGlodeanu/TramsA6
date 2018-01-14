using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.Interfaces;
using EnsureThat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TramsA6.DTOS.AuthModels;
using TramsA6.DTOS.UserModels;

namespace TramsA6.Controllers
{
    [Route("api/AuthController")]
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;


        public LoginController(IUserRepository userRepository, IAuthenticationService authenticationService,
            IConfiguration configuration)
        {
            EnsureArg.IsNotNull(userRepository);
            EnsureArg.IsNotNull(authenticationService);
            EnsureArg.IsNotNull(configuration);
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDTO model)
        {
            var email = model.Email;

            if (email == null)
            {
                return BadRequest();
            }

            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return NotFound("Login failed");
            }

            if (_authenticationService.Login(email, model.Password))
            {
                var t = CreateAccessToken(user.Id);

                return Json(t);
            }

            return BadRequest();
        }


        [HttpPost("Registration")]
        public IActionResult Registration(CreateUserDto model)
        {
            var email = model.Email;

            if (email == null)
            {
                return BadRequest();
            }

            var user = _userRepository.GetUserByEmail(email);

            if (user != null)
            {
                return BadRequest("The user with this email already exists");
            }

            //var userToSave = Domain.Entities.User.Create(model.Name, model.Password, model.Username, model.Email, 0,
            //new List<Comment>());

            user = Mapper.Map(model, user);

            _authenticationService.Register(user, model.Password);

            var userCreationResponseDto = new UserCreationResponseDTO();
            userCreationResponseDto = Mapper.Map(user, userCreationResponseDto);
            return Ok(userCreationResponseDto);
        }


        private ResponseTokenDTO CreateAccessToken(Guid userId)
        {
            var now = DateTime.Now;

            // add the registered claims for JWT (RFC7519).
            // For more info, see https://tools.ietf.org/html/rfc7519#section-4.1
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
            };

            var tokenExpirationMins =
                _configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
            var issuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Auth:Jwt:Key"]));

            var token = new JwtSecurityToken(
                _configuration["Auth:Jwt:Issuer"],
                _configuration["Auth:Jwt:Audience"],
                claims,
                now,
                now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
                new SigningCredentials(
                    issuerSigningKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new ResponseTokenDTO
            {
                Token = encodedToken,
                Expiration = tokenExpirationMins
            };
        }
    }
}