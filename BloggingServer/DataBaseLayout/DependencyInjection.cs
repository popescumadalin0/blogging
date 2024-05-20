using DataBaseLayout.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataBaseLayout;

public static class DependencyInjection
{
    public static IServiceCollection AddDataLayout(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IContext, Context>();

        var databaseConnectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<Context>(options => options.UseSqlServer(databaseConnectionString));
        return services;
    }
}