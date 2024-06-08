using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BloggingClient.Shared;

public partial class NavMenu
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationState { get; set; }

    private AuthenticationState _authState;

    private string _filter = string.Empty;

    private bool _showSearch = false;

    protected override async Task OnParametersSetAsync()
    {
        _authState = await AuthenticationState;
    }

    private void SearchBlogs()
    {
        NavigationManager.NavigateTo($"search/{_filter}");
    }
}