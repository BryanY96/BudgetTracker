using MengYu.BudgetTracker.ApplicationCore.Models;
using MengYu.BudgetTracker.ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MengYu.BudgetTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenditureController : ControllerBase
    {
        private readonly IExpenditureService _expenditureService;
        public ExpenditureController(IExpenditureService expenditureService)
        {
            _expenditureService = expenditureService;
        }

        [HttpGet]
        [Route("allexpenses")]
        public async Task<IActionResult> ListAllExpenditures()
        {
            var expenses = await _expenditureService.ListAllExpenditures();
            if (!expenses.Any())
            {
                NotFound("No expenses found");
            }
            return Ok(expenses);
        }

        [HttpPost]
        [Route("addexpense")]
        public async Task<IActionResult> AddExpenditure(ExpenditureRequestModel model)
        {
            var expense = await _expenditureService.AddExpenditure(model);
            return Ok(expense);
        }

        [HttpPut]
        [Route("updateexpense")]
        public async Task<IActionResult> UpdateExpenditure(ExpenditureRequestModel model)
        {
            var expense = await _expenditureService.UpdateExpenditure(model);
            return Ok(expense);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteExpenditure(int id)
        {
            if (!await _expenditureService.DeleteExpenditure(id))
            {
                return BadRequest("Delete expenditure failed");
            }
            return Ok("Delete expenditure successed");
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetExpendituresById(int id)
        {
            var expenses = await _expenditureService.ListExpensituresById(id);
            if (!expenses.Any())
            {
                NotFound("No expenditures exist for this user");
            }
            return Ok(expenses);
        }
    }
}
