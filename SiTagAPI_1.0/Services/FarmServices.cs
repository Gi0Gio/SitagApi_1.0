// Services/FarmServices.cs
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.DTOs;
using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class FarmServices : IFarmServices
    {
        private readonly SitagDBContext _context;

        public FarmServices(SitagDBContext context)
        {
            _context = context;
        }

        public async Task<farm?> CreateFarm(CreateFarmDto createFarm)
        {
            if (createFarm == null)
                throw new ArgumentNullException(nameof(createFarm), "Los datos de la granja no pueden ser nulos.");

            var newFarm = new farm
            {
                name = createFarm.name,
                location = createFarm.location,  // Corregido: faltaba el campo de ubicación
                userId = createFarm.userId,
                description = createFarm.description
            };

            await _context.farm.AddAsync(newFarm);
            await _context.SaveChangesAsync();

            return newFarm;
        }

        public async Task<IEnumerable<ShowFarmDto>> GetFarmsByUserId(int userId)
        {
            return await _context.farm
                .Where(f => f.userId == userId)
                .Select(f => new ShowFarmDto
                {
                    name = f.name,
                    location = f.location,
                    description = f.description
                })
                .ToListAsync();
        }

        public async Task<farm?> UpdateFarm(int id, UpdateFarmDto updateFarm)
        {
            var existingFarm = await _context.farm.FindAsync(id);
            if (existingFarm == null)
            {
                return null;  
            }

            existingFarm.name = updateFarm.name ?? existingFarm.name;
            existingFarm.location = updateFarm.location ?? existingFarm.location;
            existingFarm.description = updateFarm.description ?? existingFarm.description;

            _context.farm.Update(existingFarm);
            await _context.SaveChangesAsync();

            return existingFarm;
        }
    }
}
