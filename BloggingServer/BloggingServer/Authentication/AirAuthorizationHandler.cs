using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using BloggingServer.Services.Interfaces;

namespace BloggingServer.Authentication
{
    public class AirAuthorizationHandler : AuthorizationHandler<AuthorizationRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;

        public AirAuthorizationHandler(IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }
        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context, AuthorizationRequirement requirement)
        {
            var httpRequest = _httpContextAccessor.HttpContext!.Request;
            var token = httpRequest.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            if (!_tokenService.IsValidToken(token, requirement.RoleName))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
