﻿using Microsoft.AspNetCore.Mvc;
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


        [HttpPost("addAnimal")]
        public async Task<ActionResult<animal>> CreateAnimal(CreateAnimalDto createAnimal)
        {
            var newAnimal = await _animalServices.CreateAnimal(createAnimal);
            return CreatedAtAction(nameof(CreateAnimal), newAnimal);
        }


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

        [HttpGet("/GetAllAnimals")]
        public async Task<ActionResult<List<getAnimalDto>>> GetAllAnimals()
        {
            var animals = await _animalServices.GetAllAnimals();

            return Ok(animals);
        }


    }
}
