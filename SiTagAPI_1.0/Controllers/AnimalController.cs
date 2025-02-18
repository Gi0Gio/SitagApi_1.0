using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalServices _animalServices;
        public AnimalController(IAnimalServices animalServices)
        {
            _animalServices = animalServices;
        }


        // Endpoints

        // POST: api/Animal/addAnimal

        [HttpPost("addAnimal")]
        public async Task<ActionResult<Animal>> CreateAnimal(CreateAnimalDto createAnimal)
        {
            var newAnimal = await _animalServices.CreateAnimal(createAnimal);
            return CreatedAtAction(nameof(CreateAnimal), newAnimal);
        }

        // DELETE: api/Animal/deleteAnimal/{id}
        
        [HttpDelete("deleteAnimal/{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animalDeleted = await _animalServices.DeleteAnimal(id);

            if (!animalDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }


        // GET: api/Animal/GetAnimalById/{id}
        [HttpGet("GetAnimalById/{id}")]
        public async Task<ActionResult<getAnimalDto>> GetAnimalById(int id)
        {
            var animal = await _animalServices.GetAnimalById(id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        // GET: api/Animal/GetAllAnimals/
        [HttpGet("GetAllUserAnimals/{userId}")]
        public async Task<ActionResult<List<getAllUserAnimalsDto>>> GetAllAnimals(int userId)
        {

            try
            {
                var animals = await _animalServices.GetAllUserAnimals(userId);

               
                if (animals == null || !animals.Any())
                {
                    return NotFound(new { message = $"El usuario con ID {userId} no tiene animales registrados." });
                }

                return Ok(animals);

            } catch (Exception ex)
    {
        return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", error = ex.Message
    });
    }
        }


    }
}
