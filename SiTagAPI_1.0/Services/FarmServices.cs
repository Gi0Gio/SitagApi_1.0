using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using System;

namespace SiTagAPI_1._0.Services
{
    public class FarmServices
    {

        private readonly SitagDBContext _context;
        private readonly Random _random;
        public FarmServices(SitagDBContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task<farm?> CreateFarm(CreateFarmDto createFarm)
        {
           
            var newFarm = _context.farm.Add(new farm
            {
              
                name = createFarm.name,
                location = createFarm.location,
                userId = createFarm.userId,
                description = createFarm.description
            });
            await _context.SaveChangesAsync();
            return newFarm.Entity;
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

            existingFarm.name = updateFarm.name;
            existingFarm.location = updateFarm.location;
            existingFarm.description = updateFarm.description;
            _context.farm.Update(existingFarm);
            await _context.SaveChangesAsync();

            return existingFarm;
        }

        
    }
}
