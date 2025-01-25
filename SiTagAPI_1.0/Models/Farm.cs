namespace SiTagAPI_1._0.Models
{
    public class Farm
    {
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public int userId { get; set; }
        public string description { get; set; }

        public User User { get; set; }
    }
}
