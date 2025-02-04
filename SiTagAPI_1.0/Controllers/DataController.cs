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
    
    public class DataController : ControllerBase
    {
        private readonly IDataServices _dataServices;

        public DataController(IDataServices dataServices)
        {
            _dataServices = dataServices;
        }

        [HttpPost("addAnimalInfo")]
        public async Task<IActionResult> CreateData(CreateDataDto dataDto)
        {
            var data = await _dataServices.CreateData(dataDto);
            return CreatedAtAction(nameof(CreateData), data);
        }


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

        [HttpDelete("deleteData/{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            var data = await _dataServices.DeleteData(id);
            if (data == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
