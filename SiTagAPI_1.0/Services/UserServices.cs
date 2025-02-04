using SiTagAPI_1._0.DTOs;
using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class UserServices : IUserServices
    {

        private readonly SitagDBContext _context;

        public UserServices(SitagDBContext context)
        {
            _context = context;
        }

        public async Task<UpdateEmailDto> UpdateEmail(int id, string newEmail)
        {
            var user = await _context.user.FindAsync(id);
            if (user == null) return null;

            user.email = newEmail;
            await _context.SaveChangesAsync();

            return new UpdateEmailDto { email = user.email };
        }

        public async Task<UpdatePasswordDto> UpdatePassword(int id, string newPassword)
        {
            var user = await _context.user.FindAsync(id);
            if (user == null) return null;

            user.password = newPassword;
            await _context.SaveChangesAsync();

            return new UpdatePasswordDto { password = user.password };
        }
    }
}
