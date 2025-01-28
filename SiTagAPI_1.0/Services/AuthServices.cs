using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SiTagAPI_1._0.Services
{
    public class AuthServices
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
            var user = await _context.user.FirstOrDefaultAsync(u => u.email == loginUser.email);
            if (user == null)
            {
                return null;
            }
            if (user.password != HashPassword(loginUser.password))
            {
                return null;
            }
            return user;
        }

        public async Task<user?> Register(RegisterUserDto registerUser)
        {
         
            var newUser = _context.user.Add(new user
            {
                name = registerUser.name,
                lastName = registerUser.lastName,
                email = registerUser.email,
                password = HashPassword(registerUser.password),
                cellphone = registerUser.cellphone
            });
            await _context.SaveChangesAsync();
            return newUser.Entity;
        }

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
