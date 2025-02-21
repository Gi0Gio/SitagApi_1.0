using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IMedicalService
    {
        Task<MedicalService?> CreateService(int animalId,CreateServiceDto createService);

        Task<IEnumerable<MedicalService?>> GetPreviousServicesById(int animalId);

        Task<IEnumerable<showServiceDto>> GetAnimalsHistory(int userId);

        Task<MedicalService?> DeleteMedicalServiceById(int serviceId);

        Task<UpdateServiceDto> UpdateServiceDto(int serviceId, UpdateServiceDto updateService);
    }
}
