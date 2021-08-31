using ApplicationCore.Entities;
using MengYu.BudgetTracker.ApplicationCore.Models;
using MengYu.BudgetTracker.ApplicationCore.RepositoryInterfaces;
using MengYu.BudgetTracker.ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengYu.BudgetTracker.Infrastructure.Services
{
    public class ExpenditureService : IExpenditureService
    {
        private readonly IExpenditureRepository _expenditureRepository;
        public ExpenditureService(IExpenditureRepository expenditureRepository)
        {
            _expenditureRepository = expenditureRepository;
        }
        public async Task<ExpenditureResponseModel> AddExpenditure(ExpenditureRequestModel model)
        {
            var dbExpense = await _expenditureRepository.GetByIdAsync(model.Id);
            if (dbExpense != null)
            {
                throw new Exception($"expenditure with id {model.Id} already exists");
            }
            var expense = new Expenditure
            {
                UserId = model.UserId,
                Amount = model.Amount,
                Description = model.Description,
                ExpDate = model.ExpDate,
                Remarks = model.Remarks
            };
            var addExpense = await _expenditureRepository.AddAsync(expense);
            var expenseModel = new ExpenditureResponseModel
            {
                UserId = addExpense.UserId,
                Amount = addExpense.Amount,
                Description = addExpense.Description,
                ExpDate = addExpense.ExpDate,
                Remarks = addExpense.Remarks
            };
            return expenseModel;
        }

        public async Task<bool> DeleteExpenditure(int id)
        {
            var dbExpense = await _expenditureRepository.GetByIdAsync(id);
            if (dbExpense == null)
            {
                throw new Exception($"No expenditure exists with this id {id}");
            }
            await _expenditureRepository.DeleteAsync(dbExpense);
            return true;
        }

        public async Task<IEnumerable<ExpenditureResponseModel>> ListAllExpenditures()
        {
            var expenses = await _expenditureRepository.ListAllAsync();
            if (!expenses.Any())
            {
                throw new Exception("No expense records exist");
            }
            List<ExpenditureResponseModel> expenseResp = new List<ExpenditureResponseModel>();
            foreach (var expense in expenses)
            {
                expenseResp.Add(new ExpenditureResponseModel
                {
                    Id = expense.Id,
                    UserId = expense.UserId,
                    Amount = expense.Amount,
                    Description = expense.Description,
                    ExpDate = expense.ExpDate,
                    Remarks = expense.Remarks
                });
            }
            return expenseResp;
        }

        public async Task<IEnumerable<ExpenditureResponseModel>> ListExpensituresById(int userId)
        {
            var dbExpenses = await _expenditureRepository.ListAsync(i => i.UserId == userId);
            if (!dbExpenses.Any())
            {
                throw new Exception("No expense exists for this user");
            }
            List<ExpenditureResponseModel> expenseResp = new List<ExpenditureResponseModel>();
            foreach (var expense in dbExpenses)
            {
                expenseResp.Add(new ExpenditureResponseModel
                {
                    Id = expense.Id,
                    UserId = expense.UserId,
                    Amount = expense.Amount,
                    Description = expense.Description,
                    ExpDate = expense.ExpDate,
                    Remarks = expense.Remarks
                });
            }
            return expenseResp;
        }

        public async Task<ExpenditureResponseModel> UpdateExpenditure(ExpenditureRequestModel model)
        {
            var dbExpense = await _expenditureRepository.GetByIdAsync(model.Id);
            if (dbExpense == null)
            {
                throw new Exception($"expense with id {model.Id} doesn't exist");
            }
            var expense = new Expenditure
            {
                UserId = model.UserId,
                Amount = model.Amount,
                Description = model.Description,
                ExpDate = model.ExpDate,
                Remarks = model.Remarks
            };
            var uptExpense = await _expenditureRepository.UpdateAsync(expense);
            var expenseModel = new ExpenditureResponseModel
            {
                UserId = uptExpense.UserId,
                Amount = uptExpense.Amount,
                Description = uptExpense.Description,
                ExpDate = uptExpense.ExpDate,
                Remarks = uptExpense.Remarks
            };
            return expenseModel;
        }
    }
}
