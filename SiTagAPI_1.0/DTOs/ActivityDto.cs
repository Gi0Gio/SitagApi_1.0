namespace SiTagAPI_1._0.DTOs
{
    public class GetAllActivitiesByUserDto
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }

        public string Type { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Date { get; set; }
    }


    public class CreateActivityDto
    {
 
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
    }


    public class UpdateActivityDto
    {
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
    }

    public class SwitchGroupDto
    {
        public int AnimalId { get; set; }
        
    }


}
