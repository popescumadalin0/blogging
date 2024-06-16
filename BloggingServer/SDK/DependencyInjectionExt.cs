using System;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using SDK.Clients;
using SDK.Interfaces;

namespace SDK;

/// <summary />
public static class DependencyInjectionExt
{
    /// <summary />
    public static IServiceCollection AddBloggingApiClient(this IServiceCollection services, Uri url)
    {
        services.AddTransient<AuthenticationDelegatingHandler>();

        services.AddRefitClient<IBloggingApi>()
            .ConfigureHttpClient(c => c.BaseAddress = url).
            AddHttpMessageHandler<AuthenticationDelegatingHandler>();

        services.AddSingleton<IBloggingApiClient, BloggingApiClient>();
        return services;
    }
}