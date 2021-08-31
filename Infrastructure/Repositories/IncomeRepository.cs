using ApplicationCore.Entities;
using Infrastructure.Data;
using MengYu.BudgetTracker.ApplicationCore.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MengYu.BudgetTracker.Infrastructure.Repositories
{
    public class IncomeRepository: EfRepository<Income>, IIncomeRepository
    {
        public IncomeRepository(BudgetTrackerDbContext dbContext): base(dbContext)
        {
        }

        public async Task<decimal> GetIncomeTotalById(int userId)
        {
            var totalIncome = await _dbContext.Incomes.Where(i => i.UserId == userId).DefaultIfEmpty().SumAsync(i => i.Amount);
            return totalIncome;
        }
        

        
    }
}
