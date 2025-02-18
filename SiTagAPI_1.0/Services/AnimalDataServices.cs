using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class AnimalDataServices : IDataServices
    {

        private readonly SitagDbContext _context;
        public AnimalDataServices(SitagDbContext context)
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

        public async Task<bool> UpdateAnimalState(int animalId, int newState)
        {
            try
            {

                var lastAnimalData = await _context.AnimalData
                    .Where(ad => ad.AnimalId == animalId)
                    .OrderByDescending(ad => ad.EntryDate)
                    .FirstOrDefaultAsync();

                if (lastAnimalData == null)
                {
                    return false;
                }


                var newAnimalData = new AnimalDatum
                {
                    AnimalId = lastAnimalData.AnimalId,
                    DivisionId = lastAnimalData.DivisionId,
                    Weight = lastAnimalData.Weight,
                    State = newState,
                    EntryDate = DateTime.UtcNow
                };


                _context.AnimalData.Add(newAnimalData);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateAnimalState: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> SwitchDivision(int animalId, int newDivision)
        {
            try
            {
               var division = await _context.FarmDivisions
                    .Where(fd => fd.Id == newDivision)
                    .FirstOrDefaultAsync();
                if (division == null)
                {
                    return false;
                }

                var lastAnimalData = await _context.AnimalData
                    .Where(ad => ad.AnimalId == animalId)
                    .OrderByDescending(ad => ad.EntryDate)
                    .FirstOrDefaultAsync();

                if (lastAnimalData == null)
                {
                    return false; 
                }

                
                var farm = await (from f in _context.Farms
                                  join fd in _context.FarmDivisions on f.Id equals fd.FarmId
                                  where fd.Id == lastAnimalData.DivisionId
                                  select f)
                                  .FirstOrDefaultAsync();

                if (farm == null)
                {
                    return false;
                }

               
                var newFarmDivision = await _context.FarmDivisions
                    .Where(fd => fd.Id == newDivision)
                    .FirstOrDefaultAsync();

                if (newFarmDivision == null)
                {
                    return false; 
                }

               
                var newFarm = await _context.Farms
                    .Where(f => f.Id == newFarmDivision.FarmId)
                    .FirstOrDefaultAsync();

                if (newFarm == null || newFarm.UserId != farm.UserId)
                {
                    return false; 
                }

                
                var newAnimalData = new AnimalDatum
                {
                    AnimalId = lastAnimalData.AnimalId,
                    DivisionId = newDivision, 
                    Weight = lastAnimalData.Weight,
                    State = lastAnimalData.State,
                    EntryDate = DateTime.UtcNow
                };

                _context.AnimalData.Add(newAnimalData);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en SwitchDivision: {ex.Message}");
                return false;
            }
        }

    }
}
