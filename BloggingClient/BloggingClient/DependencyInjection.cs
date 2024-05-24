using System;
using BloggingClient.States;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
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
        services.AddCascadingAuthenticationState();

        services.AddBlazoredSessionStorage();
        services.AddBlazoredLocalStorage();

        services.AddAuthorizationCore();

        services.AddScoped<AuthenticationStateProvider, BloggingAuthenticationStateProvider>();

        var authProvider = services.BuildServiceProvider().GetService<AuthenticationStateProvider>();
        AuthBearerTokenFactory.SetBearerTokenGetterFunc((authProvider as BloggingAuthenticationStateProvider)!.GetBearerTokenAsync);

        var apiUrl = new Uri(config.GetSection("Api:BaseUrl").Value);
        services.AddBloggingApiClient(apiUrl);

        services.AddScoped<SnackbarState>();
        services.AddScoped<LoadingState>();

        return services;
    }
}