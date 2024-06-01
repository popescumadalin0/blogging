using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BloggingServer.Services.Interfaces;
using DataBaseLayout.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BloggingServer.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    private readonly ILogger<TokenService> _logger;

    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<string> GenerateTokenAsync(string username, int durationMin)
    {
        var user = _userManager.Users.FirstOrDefault(u => u.UserName == username);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Email, user.Email),
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppTokenSettings:Token").Value!));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(durationMin),
            signingCredentials: credential);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <inheritdoc />
    public bool IsValidToken(string token, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppTokenSettings:Token").Value!));
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            //todo: change this to true when you implement the refresh token
            ValidateLifetime = false,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true,
        };
        try
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);

            tokenHandler.ValidateToken(token, validationParameters, out _);

            if (!jwtToken.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == role))
            {
                throw new Exception($"Unauthorized! You are not {role}");
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return false;
        }
    }

    /// <inheritdoc />
    public int GetExpirationTimeFromJwtInMinutes(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt = tokenHandler.ReadJwtToken(token);

        var remainingTime = jwt.ValidTo.Subtract(DateTime.UtcNow);
        return (int)remainingTime.TotalMinutes;
    }
}