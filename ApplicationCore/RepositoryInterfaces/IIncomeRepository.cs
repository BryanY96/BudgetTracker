using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengYu.BudgetTracker.ApplicationCore.RepositoryInterfaces
{
    public interface IIncomeRepository: IAsyncRepository<Income>
    {
        Task<decimal> GetIncomeTotalById(int userId);
    }
}
