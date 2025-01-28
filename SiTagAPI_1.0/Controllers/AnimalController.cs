using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly AnimalServices _animalServices;
        public AnimalController(AnimalServices animalServices)
        {
            _animalServices = animalServices;
        }


        [HttpPost("addAnimal")]
        public async Task<ActionResult<animal>> CreateAnimal(CreateAnimalDto createAnimal)
        {
            var newAnimal = await _animalServices.CreateAnimal(createAnimal);
            return CreatedAtAction(nameof(CreateAnimal), newAnimal);
        }
    }
}
