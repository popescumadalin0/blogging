using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SDK;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    private readonly IAuthBearerToken _bearerToken;

    public AuthenticationDelegatingHandler(IAuthBearerToken bearerToken)
    {
        _bearerToken = bearerToken;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_bearerToken == null)
        {
            throw new Exception($"You need to implement {nameof(IAuthBearerToken)} first!");
        }

        var token = await _bearerToken.GetTokenAsync();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            token = await _bearerToken.RefreshTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                response = await base.SendAsync(request, cancellationToken);
            }
            else
            {
                return await _bearerToken.LogoutAsync();
            }
        }

        return response;
    }
}