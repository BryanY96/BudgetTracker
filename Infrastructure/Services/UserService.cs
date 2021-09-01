using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using MengYu.BudgetTracker.ApplicationCore.Models;
using MengYu.BudgetTracker.ApplicationCore.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenditureRepository _expenditureRepository;
        public UserService(IUserRepository userRepository, IIncomeRepository incomeRepository, IExpenditureRepository expenditureRepository)
        {
            _userRepository = userRepository;
            _incomeRepository = incomeRepository;
            _expenditureRepository = expenditureRepository;
        }

        public async Task<UserResponseModel> AddUser(AddUserRequestModel model)
        {
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser != null)
            {
                throw new Exception("Email already exists");
            }
            // create user entity object which needs to be passed into .AddAsync() method
            var user = new User
            {

                Email = model.Email,
                Password = model.Password,
                Fullname = model.Fullname,
                JoinedOn = DateTime.Now
            };

            var createdUser = await _userRepository.AddAsync(user);
            var userResponseModel = new UserResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                Fullname = createdUser.Fullname,
                JoinedOn = createdUser.JoinedOn
            };
            return userResponseModel;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var dbUser = await _userRepository.ListAsync(u => u.Id == userId);
            if (!dbUser.Any())
            {
                throw new Exception("This user doesn't exist, cannot be deleted!");
            }
            await _userRepository.DeleteAsync(dbUser.First());
            return true;
        }

        public async Task<UserDetailsResponseModel> GetUserDetails(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var userDetails = new UserDetailsResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                Fullname = user.Fullname,
                JoinedOn = user.JoinedOn,
                TotalIncome = await _incomeRepository.GetIncomeTotalById(user.Id),
                TotalExpenditure = await _expenditureRepository.GetExpenditureTotalById(user.Id),
                Balance = await _incomeRepository.GetIncomeTotalById(user.Id) - await _expenditureRepository.GetExpenditureTotalById(user.Id)
            };
            userDetails.Incomes = new List<IncomeResponseModel>();
            foreach (var income in user.Incomes)
            {
                userDetails.Incomes.Add(new IncomeResponseModel
                {
                    Id = income.Id,
                    UserId = income.UserId,
                    Amount = income.Amount,
                    Description = income.Description,
                    IncomeDate = income.IncomeDate,
                    Remarks = income.Remarks
                });
            }

            userDetails.Expenditures = new List<ExpenditureResponseModel>();
            foreach (var expenditure in user.Expenditures)
            {
                userDetails.Expenditures.Add(new ExpenditureResponseModel
                {
                    Id = expenditure.Id,
                    UserId = expenditure.UserId,
                    Amount = expenditure.Amount,
                    Description = expenditure.Description,
                    ExpDate = expenditure.ExpDate,
                    Remarks = expenditure.Remarks
                });
            }
            return userDetails;
        }

        public async Task<bool> HasUserById(int id)
        {
            return await _userRepository.GetExistsAsync(u => u.Id == id);
        }

        public async Task<bool> IsExisted(string email)
        {
            return await _userRepository.GetExistsAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<UserResponseModel>> ListAllUsers()
        {
            var users = await _userRepository.ListAllAsync();
            var userList = new List<UserResponseModel>();
            foreach (var user in users)
            {
                userList.Add(new UserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    JoinedOn = user.JoinedOn
                });
            }
            return userList;
        }

        public async Task<UserResponseModel> UpdateUser(AddUserRequestModel model)
        {
            var dbUser = await _userRepository.GetByIdAsync(model.Id);
            if (dbUser == null)
            {
                throw new Exception("This user dosen't exist");
            }
            var user = new User
            {
                Id = model.Id,
                Email = model.Email,
                Password = model.Password,
                Fullname = model.Fullname,
                JoinedOn = model.JoinedOn,
            };
            var uptUser = await _userRepository.UpdateAsync(user);
            var userResponseModel =  new UserResponseModel
            {
                Id = uptUser.Id,
                Fullname = uptUser.Fullname,
                Email = uptUser.Email,
                JoinedOn = uptUser.JoinedOn
            };
            return userResponseModel;
        }

    }
}
