using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IAnimalServices
    {
        Task<Animal?> CreateAnimal(CreateAnimalDto createAnimal);
        Task<bool> DeleteAnimal(int id);
        Task<getAnimalDto?> GetAnimalById(int id);
        Task<List<getAnimalDto>> GetAllAnimals();
    }
}
