using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class DataServices : IDataServices
    {

        private readonly SitagDBContext _context;
        public  DataServices(SitagDBContext context)
        {
            _context = context;
        }

        public async Task<data?> CreateData(CreateDataDto createData)
        {
            if (createData == null)
                throw new ArgumentNullException(nameof(createData), "Los datos proporcionados no pueden ser nulos.");

            var newData = new data
            {
                animal_id = createData.animal_id,
                weight = createData.weight,
                entryDate = DateTime.UtcNow,
                divisionId = createData.divisionId,
                state = createData.state
            };

            await _context.data.AddAsync(newData);
            await _context.SaveChangesAsync();

            return newData;
        }

        public async Task<IEnumerable<ShowDataDto>> GetAnimalRecord(int animalId)
        {
            if (animalId <= 0)
                throw new ArgumentException("El ID del animal debe ser mayor que cero.", nameof(animalId));

            var records = await _context.data
                .Include(d => d.division)
                .Where(d => d.animal_id == animalId)
                .Select(d => new ShowDataDto
                {
                    weight = d.weight,
                    division = d.division.name,
                    entryDate = d.entryDate,
                    state = d.state
                })
                .ToListAsync();

            return records;
        }

        public async Task<bool> DeleteData(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID proporcionado no es válido.", nameof(id));

            var data = await _context.data.FindAsync(id);
            if (data == null)
                return false;

            _context.data.Remove(data);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
