using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Request;
using Models.Response;

namespace BloggingServer.Services.Interfaces;

public interface IUserService
{
    /// <summary/>
    Task<UserLoginResponse> SignInAsync(string userName, string password);

    /// <summary/>
    Task SignOutAsync();

    /// <summary/>
    Task<IList<User>> GetUsersAsync();

    /// <summary/>
    Task<User> GetUserByCNPAsync(string CNP);

    /// <summary/>
    Task<User> GetUserByUserNameAsync(string userName);

    /// <summary/>
    Task<IdentityResult> RegisterUserAsync(User model, string password);

    /// <summary/>
    Task<IdentityResult> UpdateUserAsync(UserUpdate user);

    /// <summary/>
    Task<IdentityResult> UpdateUserEmailAsync(UserUpdate user, string token);

    /// <summary/>
    Task<IdentityResult> UpdateUserPasswordAsync(UserUpdate user);

    /// <summary/>
    Task<IdentityResult> UpdateUserPhoneNumberAsync(UserUpdate user, string token);

    /// <summary/>
    Task<IdentityResult> DeleteUserAsync(string CNP);
}