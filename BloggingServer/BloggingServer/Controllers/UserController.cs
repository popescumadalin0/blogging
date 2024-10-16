using BloggingServer.ResponseModels;
using BloggingServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Constants;
using BloggingServer.Services;
using Microsoft.AspNetCore.Http;

namespace BloggingServer.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles.Admin)]
    public async Task<IActionResult> GetUsersAsync()
    {
        try
        {
            var result = await _userService.GetUsersAsync();
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<User>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<User>>(ex));
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles.User)]
    public async Task<IActionResult> GetUserAsync(string id)
    {
        try
        {
            var result = await _userService.GetUserByIdAsync(id);

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<User>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<User>(ex));
        }
    }

    [HttpGet("username/{username}")]
    [Authorize(Roles.User)]
    public async Task<IActionResult> GetUserByUsernameAsync(string username)
    {
        try
        {
            var result = await _userService.GetUserByUserNameAsync(username);

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<User>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<User>(ex));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync(BlogLoginRequest user)
    {
        try
        {
            var result = await _userService.SignInAsync(user.Email, user.Password);

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<LoginResponse>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<IdentityResult>(ex));
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(AddUser user)
    {
        try
        {
            var result = await _userService.RegisterUserAsync(user);

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<IdentityResult>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<IdentityResult>(ex));
        }
    }

    [HttpPut]
    [Authorize(Roles.User)]
    public async Task<IActionResult> UpdateUserAsync(UpdateUser user)
    {
        try
        {
            var resultUpdate = await _userService.UpdateUserAsync(user);
            if (!resultUpdate.Succeeded)
            {
                throw new Exception(resultUpdate.Errors.FirstOrDefault()?.Description);
            }

            if (string.IsNullOrEmpty(user.NewPassword))
            {
                return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
            }

            if (string.IsNullOrEmpty(user.OldPassword))
            {
                throw new Exception("You need to provide the old password!");
            }

            var resultPassword = await _userService.UpdateUserPasswordAsync(user);
            if (!resultPassword.Succeeded)
            {
                throw new Exception(resultPassword.Errors.FirstOrDefault()?.Description);
            }

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles.Admin)]
    public async Task<IActionResult> DeleteUserAsync(string id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest request)
    {
        try
        {
            var result = await _userService.RefreshTokenAsync(request);

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<LoginResponse>(result));
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }
}