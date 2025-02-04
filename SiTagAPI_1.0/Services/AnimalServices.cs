using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class AnimalServices : IAnimalServices
    {
        private readonly SitagDBContext _context;
        public AnimalServices(SitagDBContext context)
        {
            _context = context;
        }

        public async Task<animal?> CreateAnimal(CreateAnimalDto createAnimal)
        {
            if (createAnimal == null)
                throw new ArgumentNullException(nameof(createAnimal), "Los datos del animal no pueden ser nulos.");

            var newAnimal = new animal
            {
                number = createAnimal.number,
                sex = createAnimal.sex,
                race = createAnimal.race,
                specie = createAnimal.specie,
                color = createAnimal.color,
                birthdate = createAnimal.birthdate
            };

            await _context.animal.AddAsync(newAnimal);
            await _context.SaveChangesAsync();

            return newAnimal;
        }

        public async Task<bool> DeleteAnimal(int id)
        {
            var animal = await _context.animal.FindAsync(id);
            if (animal == null)
            {
                return false; 
            }

            _context.animal.Remove(animal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<getAnimalDto?> GetAnimalById(int id)
        {
            var animal = await _context.animal.FindAsync(id);
            if (animal == null)
            {
                return null; 
            }

            return new getAnimalDto
            {
                number = animal.number,
                race = animal.race,
                sex = animal.sex,
                specie = animal.specie,
                color = animal.color,
                birthdate = animal.birthdate
            };
        }

        public async Task<List<getAnimalDto>> GetAllAnimals()
        {
            return await _context.animal
                .Select(animal => new getAnimalDto
                {
                    number = animal.number,
                    sex = animal.sex,
                    race = animal.race,
                    specie = animal.specie,
                    color = animal.color,
                    birthdate = animal.birthdate
                })
                .ToListAsync();
        }


    }
}
