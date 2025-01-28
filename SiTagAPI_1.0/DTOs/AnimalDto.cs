namespace SiTagAPI_1._0.DTOs
{
    public class CreateAnimalDto
    {
        public string number { get; set; }

        public byte sex { get; set; }

        public string race { get; set; }

        public string specie { get; set; }

        public string color { get; set; }
        public DateTime birthdate { get; set; }
    }
}
