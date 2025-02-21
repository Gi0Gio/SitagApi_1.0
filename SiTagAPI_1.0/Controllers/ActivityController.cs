using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController: ControllerBase
    {

        private readonly IActivityServices _activityServices;
        public ActivityController(IActivityServices activityServices)
        {
            _activityServices = activityServices;
        }
        // Endpoints
        // POST: api/Activity/addActivity/{animalId}    
        [HttpPost("addActivity/{animalId}")]
        public async Task<ActionResult<Activity>> CreateActivity(int animalId,CreateActivityDto createActivity)
        {
            var newActivity = await _activityServices.CreateActivity(animalId,createActivity);
            return CreatedAtAction(nameof(CreateActivity), newActivity);
        }


        // GET: api/Activity/GetAllActivitiesByUser/{UserId}
        [HttpGet("GetAllActivitiesByUser/{UserId}")]
        public async Task<ActionResult<List<GetAllActivitiesByUserDto>>> GetAllActivitiesByUser(int UserId)
        {
            var activities = await _activityServices.GetAllActivitiesByUser(UserId);
            if (activities == null || activities.Count == 0)
            {
                return NotFound($"No hay actividades registradas para el usuario con ID {UserId}");
            }
            return Ok(activities);
        }


        // PUT: api/Activity/updateActivity/{activityId}
        [HttpPut("updateActivity/{activityId}")]
        public async Task<IActionResult> UpdateActivity(int activityId, UpdateActivityDto updateActivity)
        {
            var updatedActivity = await _activityServices.UpdateActivity(activityId, updateActivity);
            if (updatedActivity == null)
            {
                return NotFound($"No se encontró la actividad con ID {activityId}");
            }
            return NoContent();
        }

        // DELETE: api/Activity/deleteActivity/{activityId}
        [HttpDelete("deleteActivity/{activityId}")]
        public async Task<IActionResult> DeleteActivity(int activityId)
        {
            await _activityServices.DeleteActivity(activityId);
            return NoContent();
        }


        // POST: api/Activity/moveAnimalsToDivision/{userId}/{currentDivisionId}/{newDivisionId}
        [HttpPost("moveAnimalsToDivision/{userId}/{currentDivisionId}/{newDivisionId}")]
        public async Task<bool> MoveAnimalsToDivision(int userId, int currentDivisionId, int newDivisionId)
        {
            return await _activityServices.MoveAnimalsToDivision(userId, currentDivisionId, newDivisionId);
        }
    }
}
