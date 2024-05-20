using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer;
using BloggingServer.Authentication;
using BloggingServer.Extensions;
using DataBaseLayout.DbContext;
using DataBaseLayout.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.Stores.ProtectPersonalData = true;
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<Context>();

builder.Services.AddScoped<ILookupProtectorKeyRing, KeyRing>();
builder.Services.AddScoped<ILookupProtector, LookupProtector>();
builder.Services.AddScoped<IPersonalDataProtector, PersonalDataProtector>();

builder.Services.AddScoped<IAuthorizationHandler, AirAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Roles.User, policy => policy.Requirements.Add(new AuthorizationRequirement(Roles.User)));
    options.AddPolicy(Roles.Admin, policy => policy.Requirements.Add(new AuthorizationRequirement(Roles.Admin)));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

await DefaultDataAsync();

app.Run();

return;

async Task DefaultDataAsync()
{
    var serviceProvider = builder.Services.BuildServiceProvider();
    var roleManager = serviceProvider.GetService<RoleManager<Role>>();
    var userManager = serviceProvider.GetService<UserManager<User>>();

    var userRole = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == Roles.User);
    if (userRole == null)
    {
        var result = await roleManager.CreateAsync(
            new Role()
            {
                Id = Roles.User,
                Name = Roles.User
            });
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }
    }

    var adminRole = await roleManager.Roles.FirstOrDefaultAsync(x => x.Id == Roles.Admin);
    if (adminRole == null)
    {
        var result = await roleManager.CreateAsync(
            new Role()
            {
                Id = Roles.Admin,
                Name = Roles.Admin
            });
        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }
    }

    var adminUser = await userManager.GetUsersInRoleAsync(Roles.Admin);
    if (!adminUser.Any())
    {
        var user = new User
        {
            Id = "admin",
            UserName = "admin",
            Email = "admin@admin.ro",
            EmailConfirmed = true,
            PhoneNumber = "0111111111",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            ProfileImage = Array.Empty<byte>(),
        };
        var result = await userManager.CreateAsync(user, "Admin1234!");

        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }

        result = await userManager.AddToRolesAsync(user, new List<string>() { Roles.User, Roles.Admin });

        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }
    }
}
