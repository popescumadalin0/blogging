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
        RefitSettings refitSettings = new()
        {
            AuthorizationHeaderValueGetter = (_, cancellationToken) => AuthBearerTokenFactory.GetBearerTokenAsync(cancellationToken)
        };
        services.AddRefitClient<IBloggingApi>(refitSettings)
            .ConfigureHttpClient(c => c.BaseAddress = url);

        services.AddSingleton<IBloggingApiClient, BloggingApiClient>();
        return services;
    }
}