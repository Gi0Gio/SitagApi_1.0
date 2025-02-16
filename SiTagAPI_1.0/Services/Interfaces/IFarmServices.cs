using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IFarmServices
    {
        Task<Farm?> CreateFarm(CreateFarmDto createFarm);
        Task<IEnumerable<ShowFarmDto>> GetFarmsByUserId(int userId);
        Task<Farm?> UpdateFarm(int id, UpdateFarmDto updateFarm);
    }
}
