using ApplicationCore.Entities;
using Infrastructure.Data;
using MengYu.BudgetTracker.ApplicationCore.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengYu.BudgetTracker.Infrastructure.Repositories
{
    public class ExpenditureRepository : EfRepository<Expenditure>, IExpenditureRepository
    {
        public ExpenditureRepository(BudgetTrackerDbContext dbContext): base(dbContext)
        {
        }
        public async Task<decimal> GetExpenditureTotalById(int userId)
        {
            var totalExpenditure = await _dbContext.Expenditures.Where(i => i.UserId == userId).DefaultIfEmpty().SumAsync(i => i.Amount);
            return totalExpenditure;
        }
    }
}
