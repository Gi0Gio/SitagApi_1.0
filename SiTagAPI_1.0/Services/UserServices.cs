using SiTagAPI_1._0.DTOs;
using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class UserServices : IUserServices
    {

        private readonly SitagDbContext _context;

        public UserServices(SitagDbContext context)
        {
            _context = context;
        }

        public async Task<UpdateEmailDto> UpdateEmail(int id, string newEmail)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.Email = newEmail;
            await _context.SaveChangesAsync();

            return new UpdateEmailDto { email = user.Email };
        }

        public async Task<UpdatePasswordDto> UpdatePassword(int id, string newPassword)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.Password = newPassword;
            await _context.SaveChangesAsync();

            return new UpdatePasswordDto { password = user.Password };
        }
    }
}
