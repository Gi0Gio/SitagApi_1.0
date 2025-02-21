using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IActivityServices
    {

        Task<Activity?> CreateActivity(int animalId,CreateActivityDto createActivity);

        Task<List<GetAllActivitiesByUserDto>> GetAllActivitiesByUser(int UserId);

        Task<UpdateActivityDto> UpdateActivity(int activityId, UpdateActivityDto updateActivity);

        Task DeleteActivity(int activityId);

        Task<bool> MoveAnimalsToDivision(int userId, int currentDivisionId, int newDivisionId);
    }
}
