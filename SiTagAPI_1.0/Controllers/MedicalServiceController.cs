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
        public async Task<IActionResult> CreateService(int animalId,CreateServiceDto serviceDto)
        {
            var service = await _serviceServices.CreateService(animalId,serviceDto);
            return CreatedAtAction(nameof(CreateService), service);
        }

        // GET: api/MedicalService/getPreviousServices/{animalId}
        [HttpGet("getPreviousServices/{animalId}")]
        public async Task<IActionResult> GetPreviousServices(int animalId)
        {
            var services = await _serviceServices.GetPreviousServicesById(animalId);
            if (services == null || !services.Any()) 
                return NotFound(new { message = "No hay servicios médicos previos para este animal." });

            
            return Ok(services);
        }

        // GET: api/MedicalService/getAnimalsHistory/{userId}
        [HttpGet("getAnimalsHistory/{userId}")]
        public async Task<IActionResult> GetAnimalsHistory(int userId)
        {
            var history = await _serviceServices.GetAnimalsHistory(userId);
            if (history == null || !history.Any())
                return NotFound(new { message = "No hay historial médico para los animales de este usuario." });
            return Ok(history);
        }


        // DELETE: api/MedicalService/deleteMedicalService/{serviceId}
        [HttpDelete("deleteMedicalService/{serviceId}")]
        public async Task<IActionResult> DeleteMedicalService(int serviceId)
        {
            var service = await _serviceServices.DeleteMedicalServiceById(serviceId);
            if (service == null)
                return NotFound(new { message = "No se encontró el servicio médico." });
            return Ok(service);
        }

        // PUT: api/MedicalService/updateService/{serviceId}
        [HttpPut("updateService/{serviceId}")]
        public async Task<IActionResult> UpdateService(int serviceId, UpdateServiceDto updateService)
        {
            var service = await _serviceServices.UpdateServiceDto(serviceId, updateService);
            if (service == null)
                return NotFound(new { message = "No se encontró el servicio médico." });
            return Ok(service);
        }




    }
}
