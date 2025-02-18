using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class AnimalServices : IAnimalServices
    {
        private readonly SitagDbContext _context;
        public AnimalServices(SitagDbContext context)
        {
            _context = context;
        }

        public async Task<Animal?> CreateAnimal(CreateAnimalDto createAnimal)
        {
            if (createAnimal == null)
                throw new ArgumentNullException(nameof(createAnimal), "Los datos del animal no pueden ser nulos.");

            var newAnimal = new Animal
            {
                Number = createAnimal.number,
                Sex = createAnimal.sex,
                Race = createAnimal.race,
                Specie = createAnimal.specie,
                Color = createAnimal.color,
                Birthdate = DateOnly.FromDateTime(createAnimal.birthdate)
            };

            await _context.Animals.AddAsync(newAnimal);
            await _context.SaveChangesAsync();

            return newAnimal;
        }

        public async Task<bool> DeleteAnimal(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return false; 
            }

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<getAnimalDto?> GetAnimalById(int id)
        {
            var animal = await (from a in _context.Animals
                                join ad in _context.AnimalData on a.Id equals ad.AnimalId
                                join fd in _context.FarmDivisions on ad.DivisionId equals fd.Id
                                where a.Id == id
                                && ad.EntryDate == ( // Obtener el registro más reciente
                                    _context.AnimalData
                                    .Where(sub => sub.AnimalId == a.Id)
                                    .OrderByDescending(sub => sub.EntryDate)
                                    .Select(sub => sub.EntryDate)
                                    .FirstOrDefault()
                                )
                                select new getAnimalDto
                                {
                                    number = a.Number,
                                    race = a.Race,
                                    sex = a.Sex,
                                    specie = a.Specie,
                                    color = a.Color,
                                    birthdate = a.Birthdate.ToDateTime(TimeOnly.MinValue),
                                    weight = ad.Weight,  // Peso del último registro en AnimalData
                                    division = fd.Name,  // Nombre de la división
                                    state = ad.State     // Estado del último registro
                                }).FirstOrDefaultAsync();
            if (animal == null)
            {
                return null;
            }
            return animal;
        }

        public async Task<List<getAllUserAnimalsDto>> GetAllUserAnimals(int userId)
        {

            try
            {
                
                var hasFarms = await _context.Farms.AnyAsync(f => f.UserId == userId);
                if (!hasFarms)
                {
                    return new List<getAllUserAnimalsDto>(); 
                }

        
               var animals = await (from f in _context.Farms
                                    join fd in _context.FarmDivisions on f.Id equals fd.FarmId
                                    join ad in _context.AnimalData on fd.Id equals ad.DivisionId
                                    join a in _context.Animals on ad.AnimalId equals a.Id
                                    where f.UserId == userId
                                    && ad.EntryDate == ( 
                                        _context.AnimalData
                                        .Where(sub => sub.AnimalId == ad.AnimalId)
                                        .OrderByDescending(sub => sub.EntryDate)
                                        .Select(sub => sub.EntryDate)
                                        .FirstOrDefault()
                                    )
                                    select new getAllUserAnimalsDto
                                    {
                                        id = a.Id,
                                        number = a.Number,
                                        farmId = f.Id,
                                        sex = a.Sex,
                                        divisionId = fd.Id
                                    }).ToListAsync();


                return animals;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetAllUserAnimals: {ex.Message}");
                return new List<getAllUserAnimalsDto>(); 
            }

        }


    }
}
