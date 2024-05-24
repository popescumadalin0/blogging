/*using System;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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

    private LoginModel _loginModel = new LoginModel();

    public void Dispose()
    {
        SnackbarState.OnStateChange -= StateHasChanged;
        LoadingState.OnStateChange -= StateHasChanged;
    }

    protected override void OnInitialized()
    {
        SnackbarState.OnStateChange += StateHasChanged;
        LoadingState.OnStateChange += StateHasChanged;
    }

    private async Task SignInAsync()
    {
        await LoadingState.ShowAsync();
        var result = await BloggingApiClient.LoginUserAsync(
            new UserLogin
            {
                Password = _loginModel.Password,
                Email = _loginModel.Username
            });

        await LoadingState.HideAsync();

        await SnackbarState.PushAsync(
            result.Success ? "User logged!" : result.ResponseMessage,
            !result.Success);

        if (result.Success)
        {
            var customProvider = (BloggingAuthenticationStateProvider)AuthenticationStateProvider;
            await customProvider.AuthenticateUserAsync(result.Response.AccessToken, result.Response.RefreshToken);

            NavigationManager.NavigateTo("/");
        }
    }
}*/