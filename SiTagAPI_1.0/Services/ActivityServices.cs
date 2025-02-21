using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class ActivityServices : IActivityServices
    {
        private readonly SitagDbContext _context;

        public ActivityServices(SitagDbContext context)
        {
            _context = context;
        }

        public async Task<Activity?> CreateActivity(int animalId,CreateActivityDto createActivity)
        {
            if (createActivity == null)
                throw new ArgumentNullException(nameof(createActivity), "Los datos proporcionados no pueden ser nulos.");
            var newActivity = new Activity
            {
                AnimalId = animalId,
                Type = createActivity.Type,
                Description = createActivity.Description,
                Date = DateOnly.FromDateTime(createActivity.Date) 
            };
            await _context.Activities.AddAsync(newActivity);
            await _context.SaveChangesAsync();
            return newActivity;
        }

        public async Task<List<GetAllActivitiesByUserDto>> GetAllActivitiesByUser(int userId)
        {
            var latestAnimalData = from ad in _context.AnimalData
                                   group ad by ad.AnimalId into grouped
                                   select new
                                   {
                                       AnimalId = grouped.Key,
                                       LastEntryDate = grouped.Max(ad => ad.EntryDate) 
                                   };

            var activities = await (from f in _context.Farms
                                    join fd in _context.FarmDivisions on f.Id equals fd.FarmId
                                    join ad in _context.AnimalData on fd.Id equals ad.DivisionId
                                    join latest in latestAnimalData on new { ad.AnimalId, ad.EntryDate } equals new { latest.AnimalId, EntryDate = latest.LastEntryDate }
                                    join a in _context.Animals on ad.AnimalId equals a.Id
                                    join act in _context.Activities on a.Id equals act.AnimalId
                                    where f.UserId == userId
                                    select new GetAllActivitiesByUserDto
                                    {
                                        Id = act.Id,
                                        AnimalId = act.AnimalId,
                                        Type = act.Type,
                                        Description = act.Description,
                                        Date = act.Date.ToDateTime(TimeOnly.MinValue) 
                                    }).ToListAsync();

            return activities;
        }


        public async Task<UpdateActivityDto> UpdateActivity(int activityId, UpdateActivityDto updateActivity)
        {
            var activity = await _context.Activities.FindAsync(activityId);
            if (activity == null)
                throw new ArgumentNullException(nameof(activity), "La actividad no existe.");
            activity.Type = updateActivity.Type;
            activity.Description = updateActivity.Description;
            activity.Date = DateOnly.FromDateTime(updateActivity.Date);
            await _context.SaveChangesAsync();
            return updateActivity;
        }



        public async Task DeleteActivity(int activityId)
        {
            var activity = await _context.Activities.FindAsync(activityId);
            if (activity == null)
                throw new ArgumentNullException(nameof(activity), "La actividad no existe.");
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> MoveAnimalsToDivision(int userId, int currentDivisionId, int newDivisionId)
        {
            try
            {
             
                if (!await IsDivisionOwnedByUser(userId, newDivisionId))
                {
                    return false;
                }

                
                var latestAnimalData = await GetLatestAnimalDataByDivision(currentDivisionId);

                if (!latestAnimalData.Any())
                {
                    return false; 
                }
                DateTime transferDate = DateTime.UtcNow;

                foreach (var animalData in latestAnimalData)
                {
                    _context.AnimalData.Add(new AnimalDatum
                    {
                        AnimalId = animalData.AnimalId,
                        DivisionId = newDivisionId, 
                        Weight = animalData.Weight,
                        State = animalData.State,
                        EntryDate = DateTime.UtcNow 
                    });

                    _context.Activities.Add(new Activity
                    {
                        AnimalId = animalData.AnimalId,
                        Type = "3",
                        Description = "Cambio de División",
                        Date = DateOnly.FromDateTime(transferDate) 
                    });
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en MoveAnimalsToDivision: {ex.Message}");
                return false;
            }
        }




        private async Task<bool> IsDivisionOwnedByUser(int userId, int divisionId)
        {
            return await _context.FarmDivisions
                .AnyAsync(fd => fd.Id == divisionId && fd.Farm.UserId == userId);
        }

       
        private async Task<List<AnimalDatum>> GetLatestAnimalDataByDivision(int divisionId)
        {
            return await (from ad in _context.AnimalData
                          where ad.DivisionId == divisionId
                          group ad by ad.AnimalId into grouped
                          select grouped.OrderByDescending(ad => ad.EntryDate).FirstOrDefault()
                         ).ToListAsync();
        }

    }
}
