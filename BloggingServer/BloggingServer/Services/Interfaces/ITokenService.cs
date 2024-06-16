using System.Threading.Tasks;

namespace BloggingServer.Services.Interfaces;

public interface ITokenService
{
    /// <summary/>
    Task<string> GenerateTokenAsync(string username, int durationMin);

    /// <summary/>
    bool IsValidToken(string token, string role = default);


    /// <summary/>
    int GetExpirationTimeFromJwtInMinutes(string token);
}