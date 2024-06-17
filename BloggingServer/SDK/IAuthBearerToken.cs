using System.Net.Http;
using System.Threading.Tasks;

namespace SDK;

public interface IAuthBearerToken
{
    /// <summary/>
    public Task<string> GetTokenAsync();

    /// <summary/>
    public Task<string> RefreshTokenAsync();

    /// <summary/>
    Task<HttpResponseMessage> LogoutAsync();
}