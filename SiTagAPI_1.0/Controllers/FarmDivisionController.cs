using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmDivisionController: ControllerBase
    {
    
        private readonly IDivisionServices _divisionServices;
        public FarmDivisionController(IDivisionServices divisionServices)
        {
            _divisionServices = divisionServices;
        }

       
        [HttpPost]
        public async Task<ActionResult<FarmDivision>> CreateDivision(CreateDivisionDto createDivision)
        {
            var newDivision = await _divisionServices.CreateDivision(createDivision);
            return CreatedAtAction(nameof(CreateDivision), newDivision);
        }

        // GET: api/FarmDivision/getDivisionsByFarmId/{farmId}
        [HttpGet("getDivisionsByFarmId/{farmId}")]
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
