// Services/FarmServices.cs
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.DTOs;
using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class FarmServices : IFarmServices
    {
        private readonly SitagDbContext _context;

        public FarmServices(SitagDbContext context)
        {
            _context = context;
        }

        public async Task<Farm?> CreateFarm(CreateFarmDto createFarm)
        {
            if (createFarm == null)
                throw new ArgumentNullException(nameof(createFarm), "Los datos de la granja no pueden ser nulos.");

            var newFarm = new Farm
            {
                Name = createFarm.name,
                Location = createFarm.location,  // Corregido: faltaba el campo de ubicación
                UserId = createFarm.userId,
                Description = createFarm.description
            };

            await _context.Farms.AddAsync(newFarm);
            await _context.SaveChangesAsync();

            return newFarm;
        }

        public async Task<IEnumerable<ShowFarmDto>> GetFarmsByUserId(int userId)
        {
            return await _context.Farms
                .Where(f => f.UserId == userId)
                .Select(f => new ShowFarmDto
                {
                    name = f.Name,
                    location = f.Location,
                    description = f.Description
                })
                .ToListAsync();
        }

        public async Task<Farm?> UpdateFarm(int id, UpdateFarmDto updateFarm)
        {
            var existingFarm = await _context.Farms.FindAsync(id);
            if (existingFarm == null)
            {
                return null;  
            }

            existingFarm.Name = updateFarm.name ?? existingFarm.Name;
            existingFarm.Location = updateFarm.location ?? existingFarm.Location;
            existingFarm.Description = updateFarm.description ?? existingFarm.Description;

            _context.Farms.Update(existingFarm);
            await _context.SaveChangesAsync();

            return existingFarm;
        }
    }
}
