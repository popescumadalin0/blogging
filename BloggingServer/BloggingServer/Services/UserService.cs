using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Constants;

namespace BloggingServer.Services;

public class UserService : IUserService
{
    private readonly SignInManager<DataBaseLayout.Models.User> _signinManager;
    private readonly UserManager<DataBaseLayout.Models.User> _userManager;
    private readonly ITokenService _tokenService;

    public UserService(SignInManager<DataBaseLayout.Models.User> signinManager, UserManager<DataBaseLayout.Models.User> userManager, ITokenService tokenService)
    {
        _signinManager = signinManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    /// <inheritdoc />
    public async Task<UserLoginResponse> SignInAsync(string userName, string password)
    {
        var isLogged = await _signinManager.PasswordSignInAsync(userName, password, false, false);

        if (isLogged.Succeeded)
        {
            var token = await _tokenService.GenerateTokenAsync(userName, 2);
            var refreshToken = await _tokenService.GenerateTokenAsync(userName, 8);

            var responseLogin = new UserLoginResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };

            return responseLogin;
        }

        throw new Exception("Email or password is incorrect!");
    }

    /// <inheritdoc />
    public async Task SignOutAsync()
    {
        await _signinManager.SignOutAsync();
    }


    /// <inheritdoc />
    public async Task<IList<User>> GetUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var response = users.Select(ModelToDto).ToList();
        return response;
    }

    /// <inheritdoc />
    public async Task<User> GetUserByCNPAsync(string CNP)
    {
        var user = await _userManager.Users.FirstAsync(s => s.Id == CNP && s.CNP == CNP);

        return ModelToDto(user);
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstAsync(s => s.UserName == userName);

        return ModelToDto(user);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> RegisterUserAsync(User model, string password)
    {
        var user = DtoToModel(model);

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return result;
        }

        result = await _userManager.AddToRoleAsync(user, Roles.User);

        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUserAsync(UserUpdate user)
    {
        var userModel = await _userManager.Users.FirstAsync(s => s.Id == user.CNP && s.CNP == user.CNP);

        userModel.FirstName = user.FirstName;
        userModel.LastName = user.LastName;
        if (user.ProfileImage != null)
        {
            userModel.ProfileImage = Convert.FromBase64String(user.ProfileImage);
        }

        var result = await _userManager.UpdateAsync(userModel);

        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUserEmailAsync(UserUpdate user, string token)
    {
        var userModel = await _userManager.Users.FirstAsync(s => s.Id == user.CNP && s.CNP == user.CNP);
        var result = await _userManager.ChangeEmailAsync(userModel, user.Email, token);
        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUserPasswordAsync(UserUpdate user)
    {
        var userModel = await _userManager.Users.FirstAsync(s => s.Id == user.CNP && s.CNP == user.CNP);

        var result = await _userManager.ChangePasswordAsync(userModel, user.OldPassword, user.NewPassword);
        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateUserPhoneNumberAsync(UserUpdate user, string token)
    {
        var userModel = await _userManager.Users.FirstAsync(s => s.Id == user.CNP && s.CNP == user.CNP);
        var result = await _userManager.ChangePhoneNumberAsync(userModel, user.PhoneNumber, token);
        return result;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> DeleteUserAsync(string CNP)
    {
        var user = await _userManager.Users.FirstAsync(s => s.Id == CNP && s.CNP == CNP);

        return await _userManager.DeleteAsync(user);
    }

    private static DataBaseLayout.Models.User DtoToModel(User model)
    {
        var entity = new DataBaseLayout.Models.User
        {
            Id = model.CNP,
            CNP = model.CNP,
            Document = Convert.FromBase64String(model.Document),
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            ProfileImage = Convert.FromBase64String(model.ProfileImage),
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            UserName = model.UserName,
        };

        return entity;
    }

    private static User ModelToDto(DataBaseLayout.Models.User model)
    {
        var dto = new User
        {
            CNP = model.CNP,
            Document = Convert.ToBase64String(model.Document),
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            ProfileImage = Convert.ToBase64String(model.ProfileImage),
            Id = model.Id,
            UserName = model.UserName,
        };

        return dto;
    }
}