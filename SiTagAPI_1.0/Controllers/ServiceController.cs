using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Services;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceServices _serviceServices;
        public ServiceController(ServiceServices serviceServices)
        {
            _serviceServices = serviceServices;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateService(CreateServiceDto serviceDto)
        {
            var service = await _serviceServices.CreateService(serviceDto);
            return CreatedAtAction(nameof(CreateService), service);
        }


    }
}
