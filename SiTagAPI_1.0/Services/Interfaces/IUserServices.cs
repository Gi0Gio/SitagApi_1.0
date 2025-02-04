using SiTagAPI_1._0.DTOs;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IUserServices
    {
        Task<UpdateEmailDto> UpdateEmail(int id, string newEmail);
        Task<UpdatePasswordDto> UpdatePassword(int id, string newPassword);
    }
}
