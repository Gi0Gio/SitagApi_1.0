using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.DTOs;
using System.Linq;  
using System.Text;
using System.Security.Cryptography;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // ---- Give context to the controller ----

        private readonly SitagContextDB _context;

        public AuthController(SitagContextDB context)
        {
            _context = context;
        }

        // ---- Endpoints ----
        // ---- api/auth/register ----
        [HttpPost("register")]
        public IActionResult Register(RegisterUserDto userDto)
        {
            var user = new User
            {
                name = userDto.name,
                lastName = userDto.lastName,
                email = userDto.email,
                password = HashPassword(userDto.password),
                cellphone = userDto.cellphone
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        // ---- api/auth/login ----
        [HttpPost("login")]
        public IActionResult Login(LoginUserDto userDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.email == userDto.email);

            if (user == null)
            {
                return NotFound();
            }

            if (user.password != HashPassword(userDto.password))
            {
                return Unauthorized();
            }

            return Ok(new
            {
                UserId = user.Id
            });
        }


        // ---- Functions ----

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
