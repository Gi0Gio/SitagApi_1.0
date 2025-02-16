namespace SiTagAPI_1._0.DTOs
{
    public class CreateDataDto
    {
        public int animal_id { get; set; }

        public string weight { get; set; }


        public int divisionId { get; set; }

        public int state { get; set; }
    }


    public class ShowDataDto
    {

        public string weight { get; set; }

        public string division { get; set; }

        public DateTime entryDate { get; set; }

        public int state { get; set; }


    }
}
