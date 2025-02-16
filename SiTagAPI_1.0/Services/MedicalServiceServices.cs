using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class MedicalServiceServices : IMedicalService
    {
        private readonly SitagDbContext _context;

        public MedicalServiceServices(SitagDbContext context)
        {
            _context = context;
        }

        public async Task<MedicalService?> CreateService(CreateServiceDto createService)
        {
            var newService = _context.MedicalServices.Add(new MedicalService
            {
                AnimalId = createService.animalId,
                Drug = createService.Drug,
                Date = DateOnly.FromDateTime(createService.date),
                Reason = createService.Reason,

            });
            await _context.SaveChangesAsync();
            return newService.Entity;
        }

        public async Task<IEnumerable<MedicalService?>> GetPreviousServices(int animalId)
        {
            return await _context.MedicalServices
                .Where(s => s.AnimalId == animalId)
                .ToListAsync();
        }
    }
}
