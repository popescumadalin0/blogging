using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BloggingClient.Shared;

public partial class NavMenu
{
    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationState { get; set; }

    private AuthenticationState _authState;

    protected override async Task OnParametersSetAsync()
    {
        _authState = await AuthenticationState;
    }
}