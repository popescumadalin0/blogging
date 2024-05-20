using Microsoft.Extensions.DependencyInjection;
using BloggingServer.Repositories;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services;
using BloggingServer.Services.Interfaces;
using DataBaseLayout;
using Microsoft.Extensions.Configuration;

namespace BloggingServer;

public static class DependencyInjection
{
    /// <summary />
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDataLayout(config);

        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ILayoverRepository, LayoverRepository>();
        services.AddScoped<IPlaneFacilityRepository, PlaneFacilityRepository>();
        services.AddScoped<IPlaneSeatRepository, PlaneSeatRepository>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ILayoverService, LayoverService>();
        services.AddScoped<IPlaneFacilityService, PlaneFacilityService>();
        services.AddScoped<IPlaneSeatService, PlaneSeatService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();

        /*var emailSettings = config.GetSection("EmailSettings");
        var defaultFromEmail = emailSettings["DefaultFromEmail"];
        var host = emailSettings["Host"];
        var port = emailSettings.GetValue<int>("Port");
        services.AddFluentEmail(defaultFromEmail);
        services.AddSingleton<ISender>(x => new SmtpSender(new SmtpClient(host, port)));

        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddTransient<IEmailSender<User>, EmailSender>();*/

        return services;
    }
}