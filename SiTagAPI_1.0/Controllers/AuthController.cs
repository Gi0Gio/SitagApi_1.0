using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthServices _authServices;

        private IConfiguration config;
        public AuthController(IAuthServices authServices, IConfiguration config)
        {
            _authServices = authServices;
            this.config = config;
        }

        // Endpoints

        // POST: api/Auth/Register

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            var user = await _authServices.Register(userDto);
            return CreatedAtAction(nameof(Register), user);
        }


        // POST: api/Auth/Login

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            var user = await _authServices.Login(userDto);
            if (user == null)
            {
                return BadRequest("Tu Correo o Contrasena estan malos, a quien le quieres robar hijupuita");
            }
            /*string jwtToken = GenerateToken(user);*/ // ya valio vrg el jwt usar cookies para guardar del lado del cliente cookie http only .
            return Ok();
            
        }


        //public void MakeCookies(IServiceCollection services) //buscar esto mas tarde
        //{
        //    services.AddDistributedMemoryCache();
        //    services.AddSession(options =>
        //    {
        //        options.IdleTimeout = TimeSpan.FromMinutes(10);
        //        options.Cookie.HttpOnly = true;
        //        options.Cookie.IsEssential = true;
        //    });
        //}

        //private string GenerateToken(User loggeduser)
        //{
        //    var claims = new[]
        //    {
        //    new Claim(ClaimTypes.Name, loggeduser.name),
        //    new Claim(ClaimTypes.Email, loggeduser.email),
        //};

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //    var securityToken = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(60),
        //        signingCredentials: creds
        //        );
        //    string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        //    return token;
        //}

    }
}
