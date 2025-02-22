using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Services
{
    internal class FinanceServices : IFinanceServices
    {
        private readonly SitagDbContext _context;

        public FinanceServices(SitagDbContext context)
        {
            _context = context;
        }

        public async Task<Finance?> CreateExpense(CreateExpenseDto createExpense)
        {
            if (createExpense == null)
                throw new ArgumentNullException(nameof(createExpense), "Los datos de la transacción no pueden ser nulos.");

            var newExpense = new Finance
            {
                UserId = createExpense.UserId,
                Amount = createExpense.Amount,
                Type = createExpense.Type,
                Sender = createExpense.Sender,
                Address = createExpense.Address,
                Date = createExpense.Date,
                Description = createExpense.Description
            };

            await _context.Finances.AddAsync(newExpense);
            await _context.SaveChangesAsync();
            return newExpense;
        }

        public async Task<Finance?> CreateIncome(CreateIncomeDto createIncome)
        {
            if (createIncome == null)
                throw new ArgumentNullException(nameof(createIncome), "Los datos de la transacción no pueden ser nulos.");

            var newIncome = new Finance
            {
                UserId = createIncome.UserId,
                Amount = createIncome.Amount,
                Sender = createIncome.Sender,
                Type = createIncome.Type,
                Address = createIncome.Address,
                Date = createIncome.Date,
                Description = createIncome.Description
            };
            await _context.Finances.AddAsync(newIncome);
            await _context.SaveChangesAsync();
            return newIncome;


        }

        public async Task<IEnumerable<ShowFinanceDto>> GetFinanceByUserId(int userId, int type)
        {
            return await _context.Finances
                .Where(f => f.UserId == userId && f.Type == type)
                .Select(f => new ShowFinanceDto
                {
                    Amount = f.Amount,
                    Sender = f.Sender,
                    Address = f.Address,
                    Date = f.Date,
                    Description = f.Description
                })
                .ToListAsync();
        }


        public async Task EliminateFinance(int id)
        {
            var finance = await _context.Finances.FindAsync(id);
            if (finance == null)
                return;
            _context.Finances.Remove(finance);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ShowAllFinanceDto>> GetAllFinanceByUserId(int userId)
        {
            return await _context.Finances
                .Where(f => f.UserId == userId)
                .Select(f => new ShowAllFinanceDto
                {
                    Type = f.Type,
                    Amount = f.Amount,
                    Sender = f.Sender,
                    Address = f.Address,
                    Date = f.Date,
                    Description = f.Description
                })
                .ToListAsync();
        }

        public async Task<Dictionary<string, string>> GetUserMonthlyBalance(int userId)
        {
            var currentDate = DateTime.UtcNow;

            var finances = await _context.Finances
                .Where(f => f.UserId == userId && f.Date.Year == currentDate.Year && f.Date.Month == currentDate.Month)
                .GroupBy(f => new { f.Date.Year, f.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalIncome = g.Where(f => f.Type == 1).Sum(f => (decimal)f.Amount),
                    TotalExpense = g.Where(f => f.Type == 0).Sum(f => (decimal)f.Amount)
                })
                .FirstOrDefaultAsync();

            if (finances == null)
            {
                return new Dictionary<string, string>
        {
            { "Mensaje", "No hay registros financieros para el mes actual." }
        };
            }

            var result = new Dictionary<string, string>
    {
        { $"Para el Mes {finances.Month:D2} del Año {finances.Year}", $"Su Balance es: {finances.TotalIncome - finances.TotalExpense} $" }
    };

            return result;
        }


        public async Task<Dictionary<string, string>> GetPreviousMonthsBalance(int userId)
        {
            var currentDate = DateTime.UtcNow;

            var finances = await _context.Finances
                .Where(f => f.UserId == userId && (f.Date.Year < currentDate.Year ||
                      (f.Date.Year == currentDate.Year && f.Date.Month < currentDate.Month)))
                .GroupBy(f => new { f.Date.Year, f.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalIncome = g.Where(f => f.Type == 1).Sum(f => f.Amount),
                    TotalExpense = g.Where(f => f.Type == 0).Sum(f => f.Amount)
                })
                .ToListAsync();
            if (finances == null || !finances.Any())
            {
                return new Dictionary<string, string>
                {
                    { "Mensaje", "No hay registros financieros para meses anteriores." }
                };
            }

            var result = finances.ToDictionary(
                f => $"{f.Year}-{f.Month:D2}",
                f => (f.TotalIncome - f.TotalExpense).ToString("F2")
            );

            return result;
        }

        public async Task<Dictionary<string, string>> GetMonthlyExpenses(int userId)
        {
            var currentDate = DateTime.UtcNow;
            var finances = await _context.Finances
                .Where(f => f.UserId == userId && f.Date.Year == currentDate.Year && f.Date.Month == currentDate.Month && f.Type == 0)
                .GroupBy(f => new { f.Date.Year, f.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalExpense = g.Sum(f => f.Amount)
                })
                .FirstOrDefaultAsync();
            if (finances == null)
            {
                return new Dictionary<string, string>
                {
                    { "Mensaje", "No hay registros de gastos para el mes actual." }
                };
            }
            var result = new Dictionary<string, string>
            {
                { $"Para el Mes {finances.Month:D2} del Año {finances.Year}", $"Sus Gastos Totales son: {finances.TotalExpense} $" }
            };
            return result;
        }

        public async Task<Dictionary<string, string>> GetMonthlyIncome(int userId)
        {
            var currentDate = DateTime.UtcNow;
            var finances = await _context.Finances
                .Where(f => f.UserId == userId && f.Date.Year == currentDate.Year && f.Date.Month == currentDate.Month && f.Type == 1)
                .GroupBy(f => new { f.Date.Year, f.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalIncome = g.Sum(f => f.Amount)
                })
                .FirstOrDefaultAsync();
            if (finances == null)
            {
                return new Dictionary<string, string>
                {
                    { "Mensaje", "No hay registros de ingresos para el mes actual." }
                };
            }
            var result = new Dictionary<string, string>
            {
                { $"Para el Mes {finances.Month:D2} del Año {finances.Year}", $"Sus Ingresos Totales son: {finances.TotalIncome} $" }
            };
            return result;
        }
    }
}
