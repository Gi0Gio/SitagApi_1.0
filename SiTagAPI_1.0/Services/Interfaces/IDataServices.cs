using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IDataServices
    {
        Task<AnimalDatum?> CreateData(CreateDataDto createData);
        Task<IEnumerable<ShowDataDto>> GetAnimalRecord(int animalId);
        Task<bool> DeleteData(int id);

        Task<bool> UpdateAnimalState(int animalId, int newState);

        Task<bool> SwitchDivision(int animalId, int newDivision);
    }
}
