using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Constants;

namespace BloggingServer.Services;

public class UserService : IUserService
{
    private readonly SignInManager<DataBaseLayout.Models.User> _signinManager;
    private readonly UserManager<DataBaseLayout.Models.User> _userManager;
    private readonly ITokenService _tokenService;

    private const int _hourInMins = 1440;
    private const int _weekInMins = 10080;

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

        if (user != null)
        {
            var isLogged = await _signinManager.CheckPasswordSignInAsync(user, password, false);

            if (isLogged.Succeeded)
            {
                var token = await _tokenService.GenerateTokenAsync(userName, _hourInMins);
                var refreshToken = await _tokenService.GenerateTokenAsync(userName, _weekInMins);

                var responseLogin = new LoginResponse
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                };

                return responseLogin;
            }
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
            JoinedDate = user.JoinedDate,
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
            JoinedDate = user.JoinedDate,
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
            JoinedDate = user.JoinedDate
        };
    }

    /// <inheritdoc />
    public async Task<IdentityResult> RegisterUserAsync(AddUser model)
    {
        if (!model.AcceptTerms)
        {
            throw new Exception("You must accept our terms and conditions!");
        }

        if (string.IsNullOrEmpty(model.ProfileImage))
        {
            model.ProfileImage = await DefaultImageAsync();
        }

        var user = new DataBaseLayout.Models.User
        {
            Id = Guid.NewGuid().ToString(),
            Email = model.Email,
            ProfileImage = Convert.FromBase64String(model.ProfileImage),
            EmailConfirmed = true,
            TwoFactorEnabled = false,
            UserName = model.Username,
            AcceptTerms = model.AcceptTerms,
            JoinedDate = DateTime.UtcNow,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }

        result = await _userManager.AddToRoleAsync(user, Roles.User);

        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUserAsync(UpdateUser user)
    {
        var userModel = await _userManager.Users.FirstAsync(s => s.UserName == user.Username);

        if (user.ProfileImage != null)
        {
            userModel.ProfileImage = Convert.FromBase64String(user.ProfileImage);
        }

        var result = await _userManager.UpdateAsync(userModel);

        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUserPasswordAsync(UpdateUser user)
    {
        var userModel = await _userManager.Users.FirstAsync(s => s.UserName == user.Username);

        var result = await _userManager.ChangePasswordAsync(userModel, user.OldPassword, user.NewPassword);

        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> DeleteUserAsync(string id)
    {
        var user = await _userManager.Users.FirstAsync(s => s.Id == id);

        return await _userManager.DeleteAsync(user);
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        if (!_tokenService.IsValidToken(request.RefreshToken))
        {
            throw new Exception("Your session has expired!");
        }

        if (_tokenService.IsValidToken(request.AccessToken) != false)
        {
            throw new Exception("Your access token is valid!");
        }

        var newToken = await _tokenService.GenerateTokenAsync(request.Username, _hourInMins);
        var newRefreshToken = await _tokenService.GenerateTokenAsync(
            request.Username,
            _tokenService.GetExpirationTimeFromJwtInMinutes(request.RefreshToken));

        return new LoginResponse()
        {
            AccessToken = newToken,
            RefreshToken = newRefreshToken
        };
    }

    private static async Task<string> DefaultImageAsync()
    {
        var image = await File.ReadAllBytesAsync(@"../DataBaseLayout/Data/default-image-profile.jpg");

        return Convert.ToBase64String(image);
    }
}