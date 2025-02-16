using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<User?> Login(LoginUserDto loginUser);
        Task<User?> Register(RegisterUserDto registerUser);
    }
}
