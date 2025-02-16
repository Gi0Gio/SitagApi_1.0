using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class AuthServices: IAuthServices
    {
        private readonly SitagDbContext _context;
        private readonly Random _random;

        public AuthServices(SitagDbContext context)
        {
            _context = context;
            _random = new Random();
        }
        public async Task<User?> Login(LoginUserDto loginUser)
        {
            if (loginUser == null || string.IsNullOrWhiteSpace(loginUser.email) || string.IsNullOrWhiteSpace(loginUser.password))
                throw new ArgumentException("El correo electrónico y la contraseña son obligatorios.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.email);
            if (user == null || user.Password != HashPassword(loginUser.password))
            {
                return null; // Retorna null si no existe o la contraseña es incorrecta
            }

            return user;
        }

        public async Task<User?> Register(RegisterUserDto registerUser)
        {
            if (registerUser == null)
                throw new ArgumentNullException(nameof(registerUser), "La información del usuario no puede ser nula.");

            if (await _context.Users.AnyAsync(u => u.Email == registerUser.email))
                throw new InvalidOperationException("El correo electrónico ya está registrado.");

            var newUser = new User
            {
                Name = registerUser.name,
                LastName = registerUser.lastName,
                Email = registerUser.email,
                Password = HashPassword(registerUser.password),
                Cellphone = registerUser.cellphone
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        private string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToHexString(hashedBytes).ToLowerInvariant();
            }
        }
    }
}
