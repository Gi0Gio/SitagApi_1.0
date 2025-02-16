using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class FarmDivisionServices: IDivisionServices
    {
        private readonly SitagDbContext _context;
        public FarmDivisionServices(SitagDbContext context)
        {
            _context = context;
        }

        public async Task<FarmDivision?> CreateDivision(CreateDivisionDto createDivision)
        {
            if (createDivision == null)
                throw new ArgumentNullException(nameof(createDivision), "Los datos de la división no pueden ser nulos.");

            var newDivision = new FarmDivision
            {
            Name = createDivision.name,
                FarmId = createDivision.farmId
            };

            await _context.FarmDivisions.AddAsync(newDivision);
            await _context.SaveChangesAsync();

            return newDivision;
        }

        public async Task<IEnumerable<ShowDivisionDto>> GetDivisionsByFarmId(int farmId)
        {
            return await _context.FarmDivisions
                .Where(d => d.FarmId == farmId)
                .Select(d => new ShowDivisionDto
                {
                    name = d.Name
                })
                .ToListAsync();
        }
    }
}
