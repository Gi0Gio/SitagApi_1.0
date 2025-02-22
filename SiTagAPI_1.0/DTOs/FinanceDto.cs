namespace SiTagAPI_1._0.DTOs
{
    public class CreateExpenseDto
    {
        public int UserId { get; set; }
        public double Amount { get; set; }

        public byte Type = 0;

        public string Sender { get; set; }

        public string? Address { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }


    public class CreateIncomeDto
    {
        public int UserId { get; set; }
        public double Amount { get; set; }
        public byte Type = 1;
        public string Sender { get; set; }
        public string? Address { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

    public class ShowFinanceDto
    {
        public double Amount { get; set; }
        public string Sender { get; set; }
        public string? Address { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

    public class ShowAllFinanceDto
    {
        public byte Type { get; set; }
        public double Amount { get; set; }
        public string Sender { get; set; }
        public string? Address { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

}
