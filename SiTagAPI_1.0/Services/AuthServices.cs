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
        private readonly SitagDBContext _context;
        private readonly Random _random;

        public AuthServices(SitagDBContext context)
        {
            _context = context;
            _random = new Random();
        }
        public async Task<user?> Login(LoginUserDto loginUser)
        {
            if (loginUser == null || string.IsNullOrWhiteSpace(loginUser.email) || string.IsNullOrWhiteSpace(loginUser.password))
                throw new ArgumentException("El correo electrónico y la contraseña son obligatorios.");

            var user = await _context.user.FirstOrDefaultAsync(u => u.email == loginUser.email);
            if (user == null || user.password != HashPassword(loginUser.password))
            {
                return null; // Retorna null si no existe o la contraseña es incorrecta
            }

            return user;
        }

        public async Task<user?> Register(RegisterUserDto registerUser)
        {
            if (registerUser == null)
                throw new ArgumentNullException(nameof(registerUser), "La información del usuario no puede ser nula.");

            if (await _context.user.AnyAsync(u => u.email == registerUser.email))
                throw new InvalidOperationException("El correo electrónico ya está registrado.");

            var newUser = new user
            {
                name = registerUser.name,
                lastName = registerUser.lastName,
                email = registerUser.email,
                password = HashPassword(registerUser.password),
                cellphone = registerUser.cellphone
            };

            await _context.user.AddAsync(newUser);
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
