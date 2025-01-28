using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services
{
    public class DivisionServices
    {
        private readonly SitagDBContext _context;
        public DivisionServices(SitagDBContext context)
        {
            _context = context;
        }

        public async Task<division?> CreateDivision(CreateDivisionDto createDivision)
        {
            var newDivision = _context.division.Add(new division
            {
                name = createDivision.name,
                farmId = createDivision.farmId
            });
            await _context.SaveChangesAsync();
            return newDivision.Entity;
        }

        public async Task<IEnumerable<ShowDivisionDto>> GetDivisionsByFarmId(int farmId)
        {
            return await _context.division
                .Where(d => d.farmId == farmId)
                .Select(d => new ShowDivisionDto
                {
                    name = d.name
                })
                .ToListAsync();
        }
    }
}
