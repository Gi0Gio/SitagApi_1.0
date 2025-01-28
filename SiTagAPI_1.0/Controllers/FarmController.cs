using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private readonly FarmServices _farmServices;

        public FarmController(FarmServices farmServices)
        {
            _farmServices = farmServices;
        }

        [HttpPost("addFarm")]
        public async Task<IActionResult> CreateFarm(CreateFarmDto farmDto)
        {
            var farm = await _farmServices.CreateFarm(farmDto);
            return CreatedAtAction(nameof(CreateFarm), farm);
        }
        [HttpGet("getFarmsByUserId/{userId}")]
        public async Task<IActionResult> GetFarmsByUserId(int userId)
        {
            var farms = await _farmServices.GetFarmsByUserId(userId);

            if (farms == null || !farms.Any())
            {
                return NotFound($"El Usuario con Id:  {userId} es pobre y no tiene fincas");
            }

            return Ok(farms);
        }


        [HttpPut("updateAnimal")]
        public async Task<IActionResult> UpdateFarm(int id, UpdateFarmDto farmDto)
        {
            var farm = await _farmServices.UpdateFarm(id, farmDto);
            if (farm == null)
            {
                return NotFound($"La finca con Id: {id} no existe");
            }
            return Ok(farm);
        }
    }
}
