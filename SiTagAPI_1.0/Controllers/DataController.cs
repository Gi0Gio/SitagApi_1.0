using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DataController : ControllerBase
    {
        private readonly DataServices _dataServices;

        public DataController(DataServices dataServices)
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
    }
}
