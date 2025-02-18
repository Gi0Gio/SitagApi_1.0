using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AnimalDataController : ControllerBase
    {
        private readonly IDataServices _dataServices;

        public AnimalDataController(IDataServices dataServices)
        {
            _dataServices = dataServices;
        }


        // Endpoints

        // POST: api/AnimalData/addAnimalInfo

        [HttpPost("addAnimalInfo")]
        public async Task<IActionResult> CreateData(CreateDataDto dataDto)
        {
            var data = await _dataServices.CreateData(dataDto);
            return CreatedAtAction(nameof(CreateData), data);
        }

        // GET: api/AnimalData/getAnimalRecord/{animalid}


        [HttpGet("getAnimalRecord/{animalid}")]

        public async Task<IActionResult> GetAnimalRecord(int animalid)
        {

            var data = await _dataServices.GetAnimalRecord(animalid);
            if (data == null || !data.Any())
            {
                return NotFound($"El Animal con Id:  {animalid} no tiene registros");
            }
            return Ok(data);
        }

        // DELETE: api/AnimalData/deleteData/{id}

        [HttpDelete("deleteData/{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var data = await _dataServices.DeleteData(id);
            if (data == false)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/AnimalData/updateState/{animalId}/{newState}
        [HttpPost("updateState/{animalId}/{newState}")]
        public async Task<IActionResult> UpdateAnimalState(int animalId, int newState)
        {
            var data = await _dataServices.UpdateAnimalState(animalId, newState);
            if (data == false)
            {
                return NotFound();
            }
            return NoContent();
        }


        // POST: api/AnimalData/switchDivision/{animalId}/{newDivision}
        [HttpPost("switchDivision/{animalId}/{newDivision}")]
        public async Task<IActionResult> SwitchDivision(int animalId, int newDivision)
        {
            var data = await _dataServices.SwitchDivision(animalId, newDivision);
            if (data == false)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
