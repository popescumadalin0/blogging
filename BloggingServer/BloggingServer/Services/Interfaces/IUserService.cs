using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models;

namespace BloggingServer.Services.Interfaces;

public interface IUserService
{
    /// <summary/>
    Task<LoginResponse> SignInAsync(string userName, string password);

    /// <summary/>
    Task<IList<User>> GetUsersAsync();

    /// <summary/>
    Task<User> GetUserByIdAsync(string id);

    /// <summary/>
    Task<User> GetUserByUserNameAsync(string userName);

    /// <summary/>
    Task<IdentityResult> RegisterUserAsync(AddUser model);

    /// <summary/>
    Task<IdentityResult> UpdateUserAsync(UpdateUser user);

    /// <summary/>
    Task<IdentityResult> UpdateUserPasswordAsync(UpdateUser user);

    /// <summary/>
    Task<IdentityResult> DeleteUserAsync(string id);
}