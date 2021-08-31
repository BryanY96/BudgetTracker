using ApplicationCore.Entities;
using MengYu.BudgetTracker.ApplicationCore.Models;
using MengYu.BudgetTracker.ApplicationCore.RepositoryInterfaces;
using MengYu.BudgetTracker.ApplicationCore.ServiceInterfaces;
using MengYu.BudgetTracker.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengYu.BudgetTracker.Infrastructure.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        public IncomeService(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }
        public async Task<IncomeResponseModel> AddIncome(IncomeRequestModel model)
        {
            var dbIncome = await _incomeRepository.GetByIdAsync(model.Id);
            if (dbIncome != null)
            {
                throw new Exception($"income with id {model.Id} already exists");
            }
            var income = new Income
            {
                UserId = model.UserId,
                Amount = model.Amount,
                Description = model.Description,
                IncomeDate = model.IncomeDate,
                Remarks = model.Remarks
            };
            var addIncome = await _incomeRepository.AddAsync(income);
            var incomeModel = new IncomeResponseModel
            {
                UserId = addIncome.UserId,
                Amount = addIncome.Amount,
                Description = addIncome.Description,
                IncomeDate = addIncome.IncomeDate,
                Remarks = addIncome.Remarks
            };
            return incomeModel;
        }

        public async Task<bool> DeleteIncome(int id)
        {
            var dbIncome = await _incomeRepository.GetByIdAsync(id);
            if (dbIncome == null)
            {
                throw new Exception($"No income exists with this id {id}");
            }
            await _incomeRepository.DeleteAsync(dbIncome);
            return true;
        }

        public async Task<IEnumerable<IncomeResponseModel>> ListAllIncomes()
        {
            var incomes = await _incomeRepository.ListAllAsync();
            if (!incomes.Any())
            {
                throw new Exception("No income records exist");
            }
            List<IncomeResponseModel> incomeResp = new List<IncomeResponseModel>();
            foreach (var income in incomes)
            {
                incomeResp.Add(new IncomeResponseModel
                {
                    Id = income.Id,
                    UserId = income.UserId,
                    Amount = income.Amount,
                    Description = income.Description,
                    IncomeDate = income.IncomeDate,
                    Remarks = income.Remarks
                });
            }
            return incomeResp;

        }

        public async Task<IEnumerable<IncomeResponseModel>> ListIncomesById(int userId)
        {
            var dbIncomes = await _incomeRepository.ListAsync(i => i.UserId == userId);
            if (!dbIncomes.Any())
            {
                throw new Exception("No income exists for this user");
            }
            List<IncomeResponseModel> incomeResp = new List<IncomeResponseModel>();
            foreach (var income in dbIncomes)
            {
                incomeResp.Add(new IncomeResponseModel
                {
                    Id = income.Id,
                    UserId = income.UserId,
                    Amount = income.Amount,
                    Description = income.Description,
                    IncomeDate = income.IncomeDate,
                    Remarks = income.Remarks
                });
            }
            return incomeResp;


        }

        public async Task<IncomeResponseModel> UpdateIncome(IncomeRequestModel model)
        {
            var dbIncome = await _incomeRepository.GetByIdAsync(model.Id);
            if (dbIncome == null)
            {
                throw new Exception($"income with id {model.Id} doesn't exist");
            }
            var income = new Income
            {
                UserId = model.UserId,
                Amount = model.Amount,
                Description = model.Description,
                IncomeDate = model.IncomeDate,
                Remarks = model.Remarks
            };
            var uptIncome = await _incomeRepository.UpdateAsync(income);
            var incomeModel = new IncomeResponseModel
            {
                UserId = uptIncome.UserId,
                Amount = uptIncome.Amount,
                Description = uptIncome.Description,
                IncomeDate = uptIncome.IncomeDate,
                Remarks = uptIncome.Remarks
            };
            return incomeModel;
        }

    }
}
