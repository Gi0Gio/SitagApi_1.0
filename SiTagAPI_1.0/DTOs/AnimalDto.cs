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

    public class getAnimalDto
    {
        public string number { get; set; }

        public byte sex { get; set; }

        public string race { get; set; }

        public string specie { get; set; }

        public string color { get; set; }
        public DateTime birthdate { get; set; }

        public string weight { get; set; }

        public string division { get; set; }

        public int state { get; set; }

    }

    public class getAllUserAnimalsDto
    {
        public int id { get; set; }
        public string number { get; set; }

        public int farmId { get; set; }

        public int divisionId { get; set; }   

        public int sex { get; set; }
    }

}
