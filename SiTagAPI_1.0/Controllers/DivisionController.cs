using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController: ControllerBase
    {
    
        private readonly DivisionServices _divisionServices;
        public DivisionController(DivisionServices divisionServices)
        {
            _divisionServices = divisionServices;
        }


        [HttpPost]
        public async Task<ActionResult<division>> CreateDivision(CreateDivisionDto createDivision)
        {
            var newDivision = await _divisionServices.CreateDivision(createDivision);
            return CreatedAtAction(nameof(CreateDivision), newDivision);
        }

        [HttpGet("getDivisionsByFarmId")]
        public async Task<ActionResult<IEnumerable<ShowDivisionDto>>> GetDivisionsByFarmId(int farmId)
        {
            var divisions = await _divisionServices.GetDivisionsByFarmId(farmId);
            if (divisions == null || !divisions.Any())
            {
                return NotFound($"La finca con Id: {farmId} no tiene divisiones");
            }
            return Ok(divisions);
        }
    }
}
