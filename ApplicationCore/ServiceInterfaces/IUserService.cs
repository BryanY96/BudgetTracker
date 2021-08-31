using ApplicationCore.Models;
using MengYu.BudgetTracker.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseModel>> ListAllUsers();
        Task<UserResponseModel> AddUser(AddUserRequestModel addUserRequestModel);
        Task<UserResponseModel> UpdateUser(AddUserRequestModel addUserRequestModel);
        Task<bool> DeleteUser(int userId);
        Task<UserDetailsResponseModel> GetUserDetails(int userId);
        Task<bool> IsExisted(string email);
        Task<bool> HasUserById(int id);
    }
}
