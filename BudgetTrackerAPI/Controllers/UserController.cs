using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetTrackerAPI.Controllers
{
    // Attribute Routing
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // api/user/allusers
        [HttpGet]
        [Route("allusers")]
        public async Task<IActionResult> ListAllUserss()
        {
            var users = await _userService.ListAllUsers();
            if (!users.Any())
            {
                NotFound("No users exist");
            }
            // automatically convert entity into JSON data (user => JSON data)
            return Ok(users);
        }

        [HttpPost]
        [Route("adduser")]
        public async Task<IActionResult> AddUser(AddUserRequestModel model)
        {
            if (await _userService.IsExisted(model.Email))
            {
                return BadRequest("Email already exists");
            }
            var userModel = await _userService.AddUser(model);
            if (userModel == null)
            {
                return BadRequest("Add user failed");
            }
            return Ok(userModel);
        }

        [HttpPut]
        [Route("updateuser")]
        public async Task<IActionResult> UpdateUser(AddUserRequestModel model)
        {
            if (!await _userService.HasUserById(model.Id))
            {
                return BadRequest($"No user exists for id {model.Id}");
            }
            var userModel = await _userService.UpdateUser(model);
            if (userModel == null)
            {
                return BadRequest("Update user failed");
            }
            return Ok(userModel);
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!await _userService.DeleteUser(id))
            {
                return BadRequest("Delete user failed");
            }
            return Ok("Delete user successed!");
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserDetailsById(int id)
        {
            var user = await _userService.GetUserDetails(id);
            if (user == null)
            {
                NotFound($"No user found with id {id}");
            }
            return Ok(user);
        }
    }
}
