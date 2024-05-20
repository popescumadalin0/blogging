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
using Models.Request;
using Models.Response;

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

    [HttpGet("{CNP}")]
    [Authorize(Roles.User)]
    public async Task<IActionResult> GetUserAsync(string CNP)
    {
        try
        {
            var result = await _userService.GetUserByCNPAsync(CNP);

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<User>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<User>(ex));
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync(UserLogin user)
    {
        try
        {
            var result = await _userService.SignInAsync(user.Email, user.Password);

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<UserLoginResponse>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(UserRegister user)
    {
        try
        {
            var result = await _userService.RegisterUserAsync(user.User, user.Password);

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<IdentityResult>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpPut]
    [Authorize(Roles.User)]
    public async Task<IActionResult> UpdateUserAsync(UserUpdate user)
    {
        try
        {
            await _userService.UpdateUserAsync(user);
            await _userService.UpdateUserEmailAsync(user, HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty));
            await _userService.UpdateUserPasswordAsync(user);
            await _userService.UpdateUserPhoneNumberAsync(user, HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty));

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{CNP}")]
    [Authorize(Roles.User)]
    public async Task<IActionResult> DeleteUserAsync(string CNP)
    {
        try
        {
            await _userService.DeleteUserAsync(CNP);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
}