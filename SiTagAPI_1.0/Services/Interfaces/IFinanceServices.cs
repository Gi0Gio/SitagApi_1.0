using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0.Services.Interfaces
{
    public interface IFinanceServices
    {
        Task<Finance?> CreateExpense(CreateExpenseDto createExpense);
        Task<Finance?> CreateIncome(CreateIncomeDto createIncome);

        Task<IEnumerable<ShowFinanceDto>> GetFinanceByUserId(int userId, int type);

        Task EliminateFinance(int id);

        Task<IEnumerable<ShowAllFinanceDto>> GetAllFinanceByUserId(int userId);

        Task<Dictionary<string, string>> GetUserMonthlyBalance(int userId);

        Task<Dictionary<string, string>> GetPreviousMonthsBalance(int userId);

        Task<Dictionary<string, string>> GetMonthlyExpenses(int userId);

        Task<Dictionary<string, string>> GetMonthlyIncome(int userId);
    }
}
