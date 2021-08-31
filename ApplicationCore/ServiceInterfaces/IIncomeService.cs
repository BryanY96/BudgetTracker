using MengYu.BudgetTracker.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengYu.BudgetTracker.ApplicationCore.ServiceInterfaces
{
    public interface IIncomeService
    {
        Task<IEnumerable<IncomeResponseModel>> ListIncomesById(int userId);
        Task<IncomeResponseModel> AddIncome(IncomeRequestModel incomeRequestModel);
        Task<IncomeResponseModel> UpdateIncome(IncomeRequestModel incomeRequestModel);
        Task<bool> DeleteIncome(int id);
        Task<IEnumerable<IncomeResponseModel>> ListAllIncomes();
    }
}
