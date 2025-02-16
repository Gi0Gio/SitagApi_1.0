using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IMedicalService
    {
        Task<MedicalService?> CreateService(CreateServiceDto createService);

        Task<IEnumerable<MedicalService?>> GetPreviousServices(int animalId);
    }
}
