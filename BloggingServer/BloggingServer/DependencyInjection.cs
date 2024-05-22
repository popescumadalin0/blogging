using BloggingServer.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using BloggingServer.Services;
using BloggingServer.Services.Interfaces;
using DataBaseLayout;
using DataBaseLayout.Models;
using Microsoft.Extensions.Configuration;

namespace BloggingServer;

public static class DependencyInjection
{
    /// <summary />
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataLayout(config);

        services.AddScoped<IRepositoryBase<Blog>, RepositoryBase<Blog>>();

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}