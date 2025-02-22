using Microsoft.AspNetCore.Mvc;
using SiTagAPI_1._0.DTOs;
using SiTagAPI_1._0.Models;
using SiTagAPI_1._0.Services.Interfaces;

namespace SiTagAPI_1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceController : ControllerBase
    {
        private readonly IFinanceServices _financeServices;
        public FinanceController(IFinanceServices financeServices)
        {
            _financeServices = financeServices;
        }

        //endpoints


        // POST api/finance/addExpense
        [HttpPost("addExpense")]
        public async Task<ActionResult<Finance>> CreateExpense([FromBody] CreateExpenseDto createExpense)
        {
            var expense = await _financeServices.CreateExpense(createExpense);
            if (expense == null)
                return BadRequest("No se pudo crear la transacción.");
            return Ok(expense);
        }
        // POST api/finance/addIncome
        [HttpPost("addIncome")]
        public async Task<ActionResult<Finance>> CreateIncome([FromBody] CreateIncomeDto createIncome)
        {
            var income = await _financeServices.CreateIncome(createIncome);
            if (income == null)
                return BadRequest("No se pudo crear la transacción.");
            return Ok(income);

        }

        // GET api/finance/getFinanceByUserId/{userId}/{type}
        [HttpGet("getFinanceByUserId/{userId}/{type}")]
        public async Task<ActionResult<IEnumerable<ShowFinanceDto>>> GetFinanceByUserId(int userId, int type)
        {
            var finances = await _financeServices.GetFinanceByUserId(userId, type);
            if (finances == null)
                return BadRequest("No se encontraron transacciones.");
            return Ok(finances);
        }

        // GET api/finance/getAllFinanceByUserId/{userId}
        [HttpGet("getAllFinanceByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<ShowAllFinanceDto>>> GetAllFinanceByUserId(int userId)
        {
            var finances = await _financeServices.GetAllFinanceByUserId(userId);
            if (finances == null)
                return BadRequest("no se encontraron transacciones.");
            return Ok(finances);
        }

        //Delete api/finance/eliminateFinance/{id}
        [HttpDelete("eliminateFinance/{id}")]
        public async Task<ActionResult> EliminateFinance(int id)
        {
            await _financeServices.EliminateFinance(id);
            return Ok();
        }
        //GET api/finance/getUserMonthlyBalance/{userId}
        [HttpGet("getUserMonthlyBalance/{userId}")]
        public async Task<ActionResult<Dictionary<string, string>>> GetUserMonthlyBalance(int userId)
        {
            var balance = await _financeServices.GetUserMonthlyBalance(userId);
            if (balance == null)
                return BadRequest("No se encontró el balance.");
            return Ok(balance);
        }


        //GET api/finance/getPreviousMonthsBalance/{userId}
        [HttpGet("getPreviousMonthsBalance/{userId}")]
        public async Task<ActionResult<Dictionary<string, string>>> GetPreviousMonthsBalance(int userId)
        {
            var balance = await _financeServices.GetPreviousMonthsBalance(userId);
            if (balance == null)
                return BadRequest("No se encontró el balance.");
            return Ok(balance);
        }

        //GET api/finance/getMonthlyExpenses/{userId}
        [HttpGet("getMonthlyExpenses/{userId}")]
        public async Task<ActionResult<Dictionary<string, string>>> GetMonthlyExpenses(int userId)
        {
            var balance = await _financeServices.GetMonthlyExpenses(userId);
            if (balance == null)
                return BadRequest("No se encontró el balance.");
            return Ok(balance);
        }

        //GET api/finance/getMonthlyIncome/{userId}
        [HttpGet("getMonthlyIncome/{userId}")]
        public async Task<ActionResult<Dictionary<string, string>>> GetMonthlyIncome(int userId)
        {
            var balance = await _financeServices.GetMonthlyIncome(userId);
            if (balance == null)
                return BadRequest("No se encontró el balance.");
            return Ok(balance);
        }
    }
}
