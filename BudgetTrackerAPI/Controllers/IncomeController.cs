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
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        [Route("allincomes")]
        public async Task<IActionResult> ListAllIncomes()
        {
            var incomes = await _incomeService.ListAllIncomes();
            if (!incomes.Any())
            {
                NotFound("No incomes found");
            }
            return Ok(incomes);
        }

        [HttpPost]
        [Route("addincome")]
        public async Task<IActionResult> AddIncome(IncomeRequestModel model)
        {
            var income = await _incomeService.AddIncome(model);
            return Ok(income);
        }

        [HttpPut]
        [Route("updateincome")]
        public async Task<IActionResult> UpdateIncome(IncomeRequestModel model)
        {
            var income = await _incomeService.UpdateIncome(model);
            return Ok(income);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            if (!await _incomeService.DeleteIncome(id))
            {
                return BadRequest("Delete income failed");
            }
            return Ok("Delete income successed");
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetIncomesById(int id)
        {
            var incomes = await _incomeService.ListIncomesById(id);
            if (!incomes.Any())
            {
                NotFound("No incomes exist for this user");
            }
            return Ok(incomes);
        }
    }
}
