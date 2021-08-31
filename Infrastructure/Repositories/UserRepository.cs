using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using MengYu.BudgetTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository: EfRepository<User>, IUserRepository
    {
        public UserRepository(BudgetTrackerDbContext dbContext): base(dbContext)
        {

        }
        //public override async Task<IEnumerable<User>> ListAllAsync()
        //{
        //    var users = await _dbContext.Users.OrderBy(u => u.Id).ToListAsync();
        //    return users;
        //}

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.Include(u => u.Incomes).Include(u => u.Expenditures).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new Exception($"No user found for the id {id}");
            }

            
            //var totalIncome = await _dbContext.Incomes.Where(i => i.UserId == id).DefaultIfEmpty().SumAsync(i => i.Amount);
            //var totalExpenditure = await _dbContext.Expenditures.Where(e => e.UserId == id).DefaultIfEmpty().SumAsync(e => e.Amount);
            //var balance = totalIncome - totalExpenditure;
            return user;
        }
    }
}
