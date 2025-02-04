using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using System.Threading.Tasks;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IDivisionServices
    {
        Task<division?> CreateDivision(CreateDivisionDto createDivision);
        Task<IEnumerable<ShowDivisionDto>> GetDivisionsByFarmId(int farmId);
    }
}
