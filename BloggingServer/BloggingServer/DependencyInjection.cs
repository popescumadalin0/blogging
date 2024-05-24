using BloggingServer.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using BloggingServer.Services;
using BloggingServer.Services.Interfaces;
using DataBaseLayout;
using DataBaseLayout.Models;
using Microsoft.Extensions.Configuration;
using BloggingServer.Repositories;

namespace BloggingServer;

public static class DependencyInjection
{
    /// <summary />
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataLayout(config);

        services.AddScoped<IRepositoryBase<Blog>, RepositoryBase<Blog>>();
        services.AddScoped<IRepositoryBase<BlogCategory>, RepositoryBase<BlogCategory>>();
        services.AddScoped<IRepositoryBase<Comment>, RepositoryBase<Comment>>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBlogCategoryService, BlogCategoryService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<ICommentService, CommentService>();

        return services;
    }
}