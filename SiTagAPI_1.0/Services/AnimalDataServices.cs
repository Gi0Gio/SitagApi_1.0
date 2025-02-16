using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class AnimalDataServices : IDataServices
    {

        private readonly SitagDbContext _context;
        public  AnimalDataServices(SitagDbContext context)
        {
            _context = context;
        }

        public async Task<AnimalDatum?> CreateData(CreateDataDto createData)
        {
            if (createData == null)
                throw new ArgumentNullException(nameof(createData), "Los datos proporcionados no pueden ser nulos.");

            var newData = new AnimalDatum
            {
                AnimalId = createData.animal_id,
                Weight = createData.weight,
                EntryDate = DateTime.UtcNow,
                DivisionId = createData.divisionId,
                State = createData.state
            };

            await _context.AnimalData.AddAsync(newData);
            await _context.SaveChangesAsync();

            return newData;
        }

        public async Task<IEnumerable<ShowDataDto>> GetAnimalRecord(int animalId)
        {
            if (animalId <= 0)
                throw new ArgumentException("El ID del animal debe ser mayor que cero.", nameof(animalId));

            var records = await _context.AnimalData
                .Where(d => d.AnimalId == animalId)
                .Select(d => new ShowDataDto
                {
                    weight = d.Weight,
                    division = d.Division.Name,
                    entryDate = d.EntryDate,
                    state = d.State
                })
                .ToListAsync();

            return records;
        }

        public async Task<bool> DeleteData(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID proporcionado no es válido.", nameof(id));

            var data = await _context.AnimalData.FindAsync(id);
            if (data == null)
                return false;

            _context.AnimalData.Remove(data);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
