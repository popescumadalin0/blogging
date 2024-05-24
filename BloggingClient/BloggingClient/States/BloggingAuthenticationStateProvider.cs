using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BloggingClient.States;

public class BloggingAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    private static string _token;
    private static string _refreshToken;

    public BloggingAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(_token))
            {
                var token = await _localStorage.GetItemAsync<string>("token");
                var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
                _token = token;
                _refreshToken = refreshToken;
            }

            if (string.IsNullOrEmpty(_token))
            {
                return new AuthenticationState(_anonymous);
            }

            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(_token);

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

    public Task<string> GetBearerTokenAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_token);
    }

    public async Task AuthenticateUserAsync(string token, string refreshToken)
    {
        await _localStorage.SetItemAsync("token", token);
        await _localStorage.SetItemAsync("refreshToken", refreshToken);

        _token = token;
        _refreshToken = token;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutUserAsync()
    {
        await _localStorage.SetItemAsync("token", string.Empty);
        await _localStorage.SetItemAsync("refreshToken", string.Empty);

        _token = string.Empty;
        _refreshToken = string.Empty;

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}