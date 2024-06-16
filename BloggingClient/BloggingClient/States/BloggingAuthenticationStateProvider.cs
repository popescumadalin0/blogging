using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BloggingClient.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Models;

namespace BloggingClient.States;

public class BloggingAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ISessionStorage _storage;

    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public BloggingAuthenticationStateProvider(ISessionStorage storage)
    {
        _storage = storage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = _storage.GetItem("token");

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(_anonymous);
            }

            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(
                decodedToken.Claims.ToList(),
                "jwt");

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
        catch (Exception)
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task<LoginResponse> GetTokensAsync()
    {
        var token = _storage.GetItem("token");
        var refreshToken = _storage.GetItem("refreshToken");

        return new LoginResponse()
        {
            AccessToken = token,
            RefreshToken = refreshToken,
        };
    }

    public async Task AuthenticateOrUpdateUserAsync(string token, string refreshToken)
    {
        _storage.SetItem("token", token);
        _storage.SetItem("refreshToken", refreshToken);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutUserAsync()
    {
        _storage.SetItem("token", string.Empty);
        _storage.SetItem("refreshToken", string.Empty);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}