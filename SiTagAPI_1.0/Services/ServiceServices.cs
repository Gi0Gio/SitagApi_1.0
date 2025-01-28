using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services
{
    public class ServiceServices
    {
        private readonly SitagDBContext _context;

        public ServiceServices(SitagDBContext context)
        {
            _context = context;
        }

        public async Task<service?> CreateService(CreateServiceDto createService)
        {
            var newService = _context.service.Add(new service
            {
                animalId = createService.animalId,
                type = createService.type,
                description = createService.description,
                date = createService.date
            });
            await _context.SaveChangesAsync();
            return newService.Entity;
        }
    }
}
