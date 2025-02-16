using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IDataServices
    {
        Task<AnimalDatum?> CreateData(CreateDataDto createData);
        Task<IEnumerable<ShowDataDto>> GetAnimalRecord(int animalId);
        Task<bool> DeleteData(int id);
    }
}
