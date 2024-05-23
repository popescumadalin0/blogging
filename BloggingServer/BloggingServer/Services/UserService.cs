using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Constants;

namespace BloggingServer.Services;

public class UserService : IUserService
{
    private readonly SignInManager<DataBaseLayout.Models.User> _signinManager;
    private readonly UserManager<DataBaseLayout.Models.User> _userManager;
    private readonly ITokenService _tokenService;

    public UserService(
        SignInManager<DataBaseLayout.Models.User> signinManager,
        UserManager<DataBaseLayout.Models.User> userManager,
        ITokenService tokenService)
    {
        _signinManager = signinManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    /// <inheritdoc />
    public async Task<LoginResponse> SignInAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        var isLogged = await _signinManager.CheckPasswordSignInAsync(user, password, false);

        if (isLogged.Succeeded)
        {
            var token = await _tokenService.GenerateTokenAsync(userName, 2);
            var refreshToken = await _tokenService.GenerateTokenAsync(userName, 8);

            var responseLogin = new LoginResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };

            return responseLogin;
        }

        throw new Exception("Email or password is incorrect!");
    }

    /// <inheritdoc />
    public async Task<IList<User>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var response = users.Select(user => new User()
        {
            Username = user.UserName,
            Email = user.Email,
            Id = user.Id,
            ProfileImage = Convert.ToBase64String(user.ProfileImage),
            NumberOfBlogs = user.Blogs.Count,
        }).ToList();

        return response;
    }

    /// <inheritdoc />
    public async Task<User> GetUserByIdAsync(string id)
    {
        var user = await _userManager.Users.FirstAsync(s => s.Id == id);

        return new User()
        {
            Username = user.UserName,
            Email = user.Email,
            Id = user.Id,
            ProfileImage = Convert.ToBase64String(user.ProfileImage),
            NumberOfBlogs = user.Blogs.Count,
        };
    }

    /// <inheritdoc />
    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstAsync(s => s.UserName == userName);

        return new User()
        {
            Username = user.UserName,
            Email = user.Email,
            Id = user.Id,
            ProfileImage = Convert.ToBase64String(user.ProfileImage),
            NumberOfBlogs = user.Blogs.Count,
        };
    }

    /// <inheritdoc />
    public async Task<IdentityResult> RegisterUserAsync(AddUser model)
    {
        var user = new DataBaseLayout.Models.User
        {
            Id = model.Id,
            Email = model.Email,
            ProfileImage = Convert.FromBase64String(model.ProfileImage),
            EmailConfirmed = true,
            TwoFactorEnabled = false,
            UserName = model.Username,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return result;
        }

        result = await _userManager.AddToRoleAsync(user, Roles.User);

        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUserAsync(UpdateUser user)
    {
        var userModel = await _userManager.Users.FirstAsync(s => s.Id == user.Id);

        if (user.ProfileImage != null)
        {
            userModel.ProfileImage = Convert.FromBase64String(user.ProfileImage);
        }

        var result = await _userManager.UpdateAsync(userModel);

        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUserEmailAsync(UpdateUser user, string token)
    {
        var userModel = await _userManager.Users.FirstAsync(s => s.Id == user.Id);
        var result = await _userManager.ChangeEmailAsync(userModel, user.Email, token);
        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUserPasswordAsync(UpdateUser user)
    {
        var userModel = await _userManager.Users.FirstAsync(s => s.Id == user.Id);

        var result = await _userManager.ChangePasswordAsync(userModel, user.OldPassword, user.NewPassword);
        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> DeleteUserAsync(string id)
    {
        var user = await _userManager.Users.FirstAsync(s => s.Id == id);

        return await _userManager.DeleteAsync(user);
    }
}