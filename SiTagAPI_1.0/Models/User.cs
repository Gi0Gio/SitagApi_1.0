namespace SiTagAPI_1._0.Models
{
    public class User
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string cellphone { get; set; }

        public ICollection<Farm> Farms { get; set; }    
    }
}
