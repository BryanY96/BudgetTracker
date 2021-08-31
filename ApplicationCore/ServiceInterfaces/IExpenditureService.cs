using MengYu.BudgetTracker.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengYu.BudgetTracker.ApplicationCore.ServiceInterfaces
{
    public interface IExpenditureService
    {
        Task<IEnumerable<ExpenditureResponseModel>> ListExpensituresById(int userId);
        Task<ExpenditureResponseModel> AddExpenditure(ExpenditureRequestModel expenditureRequestModel);
        Task<ExpenditureResponseModel> UpdateExpenditure(ExpenditureRequestModel expenditureRequestModel);
        Task<bool> DeleteExpenditure(int id);
        Task<IEnumerable<ExpenditureResponseModel>> ListAllExpenditures();
    }
}
