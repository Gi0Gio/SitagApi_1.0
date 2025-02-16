using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Services;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalServiceController : ControllerBase
    {
        private readonly IMedicalService _serviceServices;
        public MedicalServiceController(IMedicalService serviceServices)
        {
            _serviceServices = serviceServices;
        }

        // Endpoints

        // POST: api/MedicalService/addMedicaLService
        [HttpPost("addMedicaLService")]
        public async Task<IActionResult> CreateService(CreateServiceDto serviceDto)
        {
            var service = await _serviceServices.CreateService(serviceDto);
            return CreatedAtAction(nameof(CreateService), service);
        }

        // GET: api/MedicalService/getPreviousServices/{animalId}
        [HttpGet("getPreviousServices/{animalId}")]
        public async Task<IActionResult> GetPreviousServices(int animalId)
        {
            var services = await _serviceServices.GetPreviousServices(animalId);
            if (services == null || !services.Any()) 
                return NotFound(new { message = "No hay servicios médicos previos para este animal." });

            
            return Ok(services);
        }




    }
}
