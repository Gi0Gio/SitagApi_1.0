using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class DivisionServices: IDivisionServices
    {
        private readonly SitagDBContext _context;
        public DivisionServices(SitagDBContext context)
        {
            _context = context;
        }

        public async Task<division?> CreateDivision(CreateDivisionDto createDivision)
        {
            if (createDivision == null)
                throw new ArgumentNullException(nameof(createDivision), "Los datos de la división no pueden ser nulos.");

            var newDivision = new division
            {
                name = createDivision.name,
                farmId = createDivision.farmId
            };

            await _context.division.AddAsync(newDivision);
            await _context.SaveChangesAsync();

            return newDivision;
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
