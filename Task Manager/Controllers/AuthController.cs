using Microsoft.AspNetCore.Mvc;
using Task_Manager.Models;
using Task_Manager.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

       
        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            var existingUser = AppData.users.FirstOrDefault(u => u.Name == newUser.Name);
            if (existingUser != null)
                return BadRequest("User already exists");

            newUser.Id = AppData.users.Count + 1;

            if (string.IsNullOrEmpty(newUser.Role))
                newUser.Role = "User";  

            AppData.users.Add(newUser);

            return Ok("User registered successfully");
        }

        
        [HttpPost("login")]
        public IActionResult Login(LoginDto login)
        {
            var user = AppData.users.FirstOrDefault(u =>
                u.Name == login.Name && u.Password == login.Password);

            if (user == null)
                return Unauthorized("Invalid username or password");

            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserId", user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                Token = tokenHandler.WriteToken(token),
                Message = "Login successful"
            });
        }
    }
}