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

        public async Task<MedicalService?> CreateService(int animalId,CreateServiceDto createService)
        {
            var newService = _context.MedicalServices.Add(new MedicalService
            {
                AnimalId = animalId,
                Drug = createService.Drug,
                Date = DateOnly.FromDateTime(createService.date),
                Reason = createService.Reason,

            });
            await _context.SaveChangesAsync();
            return newService.Entity;
        }

        public async Task<IEnumerable<MedicalService?>> GetPreviousServicesById(int animalId)
        {
            return await _context.MedicalServices
                .Where(s => s.AnimalId == animalId)
                .ToListAsync();
        }

        public async Task<IEnumerable<showServiceDto>> GetAnimalsHistory(int userId)
        {
            try
            {
                var medicalHistory = await (from f in _context.Farms
                                            join fd in _context.FarmDivisions on f.Id equals fd.FarmId
                                            join ad in _context.AnimalData on fd.Id equals ad.DivisionId
                                            join a in _context.Animals on ad.AnimalId equals a.Id
                                            join ms in _context.MedicalServices on a.Id equals ms.AnimalId
                                            where f.UserId == userId
                                            select new showServiceDto
                                            {
                                                animalId = a.Id,
                                                Number = a.Number,
                                                Drug = ms.Drug,
                                                Reason = ms.Reason,
                                                date = ms.Date.ToDateTime(TimeOnly.MinValue) // Convertir DateOnly a DateTime
                                            })
                                            .OrderByDescending(m => m.date) // Ordenar por fecha (más reciente primero)
                                            .ToListAsync();

                return medicalHistory;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetMedicalHistoryByUserId: {ex.Message}");
                return new List<showServiceDto>(); // Retorna lista vacía en caso de error
            }
        }


        public async Task<MedicalService?> DeleteMedicalServiceById(int serviceId)
        {
            var service = await _context.MedicalServices.FindAsync(serviceId);
            if (service == null)
            {
                return null;
            }
           var deletedService = _context.MedicalServices.Remove(service);
            await _context.SaveChangesAsync();
            return deletedService.Entity;
        }


        public async Task<UpdateServiceDto> UpdateServiceDto(int serviceId, UpdateServiceDto updateService)
        {
            var service = await _context.MedicalServices.FindAsync(serviceId);
            if (service == null)
            {
                return null;
            }
            service.Drug = updateService.Drug;
            service.Reason = updateService.Reason;
            service.Date = DateOnly.FromDateTime(updateService.date);
            await _context.SaveChangesAsync();
            return updateService;
        }
    }
}
