namespace SiTagAPI_1._0.DTOs
{
    public class RegisterUserDto
    {
        public string name { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string cellphone { get; set; }
    }

    public class LoginUserDto
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
