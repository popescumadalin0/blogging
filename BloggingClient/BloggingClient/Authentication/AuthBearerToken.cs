using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BloggingClient.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using SDK;

namespace BloggingClient.Authentication;

public class AuthBearerToken : IAuthBearerToken
{
    private readonly BloggingAuthenticationStateProvider _authenticationStateProvider;
    private readonly NavigationManager _navigationManager;
    private readonly IConfiguration _config;

    public AuthBearerToken(
        AuthenticationStateProvider authenticationStateProvider,
        IConfiguration config,
        NavigationManager navigationManager)
    {
        _config = config;
        _navigationManager = navigationManager;
        _authenticationStateProvider = authenticationStateProvider as BloggingAuthenticationStateProvider;
    }

    public async Task<string> GetTokenAsync()
    {
        var tokens = await _authenticationStateProvider.GetTokensAsync();
        return tokens.AccessToken;
    }

    public async Task<string> RefreshTokenAsync()
    {
        var result = string.Empty;

        var tokens = await _authenticationStateProvider.GetTokensAsync();
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var username = authState.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;

        var baseUrl = _config.GetSection("Api:BaseUrl").Value;

        var client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl!);

        var content = new RefreshTokenRequest()
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            Username = username
        };

        var json = JsonConvert.SerializeObject(content);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/user/refresh-token", data);

        if (response.IsSuccessStatusCode)
        {
            var refreshResponse = await response.Content.ReadAsStringAsync();

            var refreshTokens = JsonConvert.DeserializeObject<LoginResponse>(refreshResponse);

            await _authenticationStateProvider.AuthenticateOrUpdateUserAsync(
                refreshTokens!.AccessToken,
                refreshTokens.RefreshToken);

            result = refreshTokens.AccessToken;
        }

        client.Dispose();

        return result;
    }

    public async Task<HttpResponseMessage> LogoutAsync()
    {
        await _authenticationStateProvider.LogoutUserAsync();

        return new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.Unauthorized,
            Content = new StringContent("Your session has expired!")
        };
    }
}