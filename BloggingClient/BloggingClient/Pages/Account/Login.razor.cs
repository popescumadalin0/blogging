using System;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using SDK.Interfaces;

namespace BloggingClient.Pages.Account;

public partial class Login : ComponentBase, IDisposable
{
    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    private IBloggingApiClient BloggingApiClient { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationState { get; set; }

    private LoginModel _loginModel = new LoginModel();

    public void Dispose()
    {
        SnackbarState.OnStateChange -= StateHasChanged;
        LoadingState.OnStateChange -= StateHasChanged;
    }

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationState;
        if (authState.User.Identity.IsAuthenticated)
        {
            NavigationManager.NavigateTo("/");
        }

        SnackbarState.OnStateChange += StateHasChanged;
        LoadingState.OnStateChange += StateHasChanged;
    }

    private async Task SignInAsync()
    {
        await LoadingState.ShowAsync();
        var result = await BloggingApiClient.LoginUserAsync(
            new BlogLoginRequest()
            {
                Password = _loginModel.Password,
                Email = _loginModel.Username
            });

        await LoadingState.HideAsync();

        if (result.Success)
        {
            var customProvider = (BloggingAuthenticationStateProvider)AuthenticationStateProvider;
            await customProvider.AuthenticateUserAsync(result.Response.AccessToken, result.Response.RefreshToken);

            NavigationManager.NavigateTo("/");
        }

        await SnackbarState.PushAsync(
            result.Success ? "User logged!" : result.ResponseMessage,
            !result.Success);
    }
}