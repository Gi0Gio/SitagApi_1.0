using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services
{
    public class AnimalServices
    {
        private readonly SitagDBContext _context;
        public AnimalServices(SitagDBContext context)
        {
            _context = context;
        }

        public async Task<animal?> CreateAnimal(CreateAnimalDto createAnimal)
        {
            var newAnimal = _context.animal.Add(new animal
            {
                number = createAnimal.number,
                sex = createAnimal.sex,
                race = createAnimal.race,
                specie = createAnimal.specie,
                color = createAnimal.color,
                birthdate = createAnimal.birthdate
            });
            await _context.SaveChangesAsync();
            return newAnimal.Entity;

        }

    }
}
