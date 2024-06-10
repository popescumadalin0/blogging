using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
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
            AuthorizationHeaderValueGetter = (_, cancellationToken) => AuthBearerTokenFactory.GetBearerTokenAsync(cancellationToken),
        };
        services.AddRefitClient<IBloggingApi>(refitSettings)
            .ConfigureHttpClient(c => c.BaseAddress = url);

        services.AddSingleton<IBloggingApiClient, BloggingApiClient>();
        return services;
    }

    public static AsyncRetryPolicy<HttpResponseMessage> GetUnauthPolicy()
    {
        return Policy.HandleResult<HttpResponseMessage>(
                r => r.StatusCode == HttpStatusCode.Unauthorized)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(1), onRetryAsync: async (response, timespan, retryNo, context) =>
            {
                if (response.Result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    
                }
            });
    }
}