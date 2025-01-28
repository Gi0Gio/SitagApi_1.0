using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services
{
    public class DataServices
    {

        private readonly SitagDBContext _context;
        public DataServices(SitagDBContext context)
        {
            _context = context;
        }

        public async Task<data?> CreateData(CreateDataDto createData)
        {
            var newData = _context.data.Add(new data
            {
                animal_id = createData.animal_id,
                weight = createData.weight,
                entryDate = DateTime.Now,
                divisionId = createData.divisionId,
                state = createData.state
            });
            await _context.SaveChangesAsync();
            return newData.Entity;
        }

        public async Task<IEnumerable<ShowDataDto>> GetAnimalRecord(int animalid)
        {

            return await _context.data
                .Include(d => d.division)
                .Where(d => d.animal_id == animalid)
                .Select(d => new ShowDataDto
                {
                    weight = d.weight,
                    division = d.division.name,
                    entryDate = d.entryDate,
                    state = d.state
                }).ToListAsync();
        }

    }
}
