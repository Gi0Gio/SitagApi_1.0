
namespace SiTagAPI_1._0.DTOs
{
    public class CreateServiceDto
    {
        public int animalId { get; set; }

        public string Drug { get; set; }

        
        public string Reason { get; set; }

       
        public DateTime date { get; set; }
    }


    public class showServiceDto
    {
        public int animalId { get; set; }

        public string Number { get; set; }
        public string Drug { get; set; }
        public string Reason { get; set; }
        public DateTime date { get; set; }
    }


    public class UpdateServiceDto
    {
        public string Drug { get; set; }
        public string Reason { get; set; }
        public DateTime date { get; set; }
    }
}
