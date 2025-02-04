using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IFarmServices
    {
        Task<farm?> CreateFarm(CreateFarmDto createFarm);
        Task<IEnumerable<ShowFarmDto>> GetFarmsByUserId(int userId);
        Task<farm?> UpdateFarm(int id, UpdateFarmDto updateFarm);
    }
}
