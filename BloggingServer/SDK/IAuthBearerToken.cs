using System.Threading.Tasks;

namespace SDK;

public interface IAuthBearerToken
{
    public Task<string> GetTokenAsync();

    public Task<string> RefreshTokenAsync();
}