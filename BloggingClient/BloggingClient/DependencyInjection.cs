using System;
using BloggingClient.States;
using BloggingClient.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SDK;

namespace BloggingClient;

/// <summary />
public static class DependencyInjection
{
    /// <summary />
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<ISessionStorage, SessionStorage>();

        services.AddAuthorizationCore();

        services.AddScoped<AuthenticationStateProvider, BloggingAuthenticationStateProvider>();

        services.AddTransient<IAuthBearerToken, AuthBearerToken>();

        services.AddCascadingAuthenticationState();

        var apiUrl = new Uri(config.GetSection("Api:BaseUrl").Value);
        services.AddBloggingApiClient(apiUrl);

        services.AddScoped<SnackbarState>();
        services.AddScoped<LoadingState>();

        return services;
    }
}