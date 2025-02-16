using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Services;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        // Endpoints

        // PUT: api/User/updateEmail/{id}
        [HttpPut("updateEmail/{id}")]
        public async Task<ActionResult<UpdateEmailDto>> UpdateEmail(int id, string newEmail)
        {
            var result = await _userServices.UpdateEmail(id, newEmail);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
        // PUT: api/User/updatePassword/{id}
        [HttpPut("updatePassword/{id}")]
        public async Task<ActionResult<UpdatePasswordDto>> UpdatePassword(int id, string newPassword)
        {
            var result = await _userServices.UpdatePassword(id, newPassword);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
    }
}
