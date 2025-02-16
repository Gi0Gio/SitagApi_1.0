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
            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return null; 
            }

            return new getAnimalDto
            {
                number = animal.Number,
                race = animal.Race,
                sex = animal.Sex,
                specie = animal.Specie,
                color = animal.Color,
                birthdate = animal.Birthdate.ToDateTime(TimeOnly.MinValue)
            };
        }

        public async Task<List<getAnimalDto>> GetAllAnimals()
        {
            return await _context.Animals
                .Select(animal => new getAnimalDto
                {
                    number = animal.Number,
                    sex = animal.Sex,
                    race = animal.Race,
                    specie = animal.Specie,
                    color = animal.Color,
                    birthdate = animal.Birthdate.ToDateTime(TimeOnly.MinValue)
                })
                .ToListAsync();
        }


    }
}
