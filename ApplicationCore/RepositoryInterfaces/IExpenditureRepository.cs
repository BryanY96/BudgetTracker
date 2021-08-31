using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengYu.BudgetTracker.ApplicationCore.RepositoryInterfaces
{
    public interface IExpenditureRepository: IAsyncRepository<Expenditure>
    {
        Task<decimal> GetExpenditureTotalById(int userId);
    }
}
