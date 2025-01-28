namespace SiTagAPI_1._0.DTOs
{
    public class CreateFarmDto
    {
        public string name { get; set; }

        public string location { get; set; }

        public int userId { get; set; }

        public string description { get; set; }
    }

    public class UpdateFarmDto
    {
        public string name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
    }

    public class ShowFarmDto
    {
        public string name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
    }
}
