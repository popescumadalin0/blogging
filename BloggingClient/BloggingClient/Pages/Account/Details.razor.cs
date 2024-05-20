using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Models.Request;
using SDK.Interfaces;

namespace BloggingClient.Pages.Account;

public partial class Details : ComponentBase, IDisposable
{
    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private IBloggingApiClient BloggingApiClient { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationState { get; set; }

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private AccountDetailsModel _user = new();

    private AccountDetailsModel _oldUser = new();

    private int _showProfileEdit;
    private int _showFirstNameEdit;
    private int _showLastNameEdit;
    private int _showEmailEdit;
    private int _showPhoneNumberEdit;
    private int _showPasswordEdit;

    public void Dispose()
    {
        SnackbarState.OnStateChange -= StateHasChanged;
        LoadingState.OnStateChange -= StateHasChanged;
    }

    protected override async Task OnInitializedAsync()
    {
        SnackbarState.OnStateChange += StateHasChanged;
        LoadingState.OnStateChange += StateHasChanged;

        await LoadingState.ShowAsync();

        var authState = await AuthenticationState;

        var user = await BloggingApiClient.GetUserAsync(
            authState.User.Claims.First(uc => uc.Type == ClaimTypes.SerialNumber).Value);

        if (!user.Success)
        {
            await SnackbarState.PushAsync(user.ResponseMessage, !user.Success);
            await LoadingState.HideAsync();
            return;
        }

        _user = new AccountDetailsModel()
        {
            ProfileImage = user.Response.ProfileImage,
            FirstName = user.Response.FirstName,
            LastName = user.Response.LastName,
            CNP = user.Response.CNP,
            Email = user.Response.Email,
            PhoneNumber = user.Response.PhoneNumber,
            UserName = user.Response.UserName,
            NewPassword = string.Empty,
            OldPassword = string.Empty
        };

        _oldUser = new AccountDetailsModel(_user);

        await LoadingState.HideAsync();
    }

    private async Task OnImageUploaded(FileUploadEventArgs e)
    {
        try
        {
            using var result = new MemoryStream();
            await e.File.OpenReadStream(long.MaxValue).CopyToAsync(result);

            _user.ProfileImage = Convert.ToBase64String(result.ToArray());
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.Message);
        }
        finally
        {
            StateHasChanged();
        }
    }

    private async Task SaveAsync()
    {
        await LoadingState.ShowAsync();

        var user = new UserUpdate()
        {
            CNP = _user.CNP,
            Email = _user.Email,
            PhoneNumber = _user.PhoneNumber,
            ProfileImage = _user.ProfileImage,
            FirstName = _user.FirstName,
            LastName = _user.LastName,
            NewPassword = _user.NewPassword,
            OldPassword = _user.OldPassword
        };

        var response = await BloggingApiClient.UpdateUserAsync(user);

        if (!response.Success)
        {
            await SnackbarState.PushAsync(response.ResponseMessage, !response.Success);
            await LoadingState.HideAsync();
            return;
        }

        _oldUser = new AccountDetailsModel(_user);

        await LoadingState.HideAsync();

        await SnackbarState.PushAsync("Successfully updated!");
    }

    private async Task LogoutAsync()
    {
        await LoadingState.ShowAsync();
        await LoadingState.HideAsync();

        var customProvider = (AirFLightsAuthenticationStateProvider)AuthenticationStateProvider;
        await customProvider.LogoutUserAsync();

        await SnackbarState.PushAsync("Successfully logout!");

        NavigationManager.NavigateTo("/login");
    }

    private async Task DeleteAsync()
    {
        await LoadingState.ShowAsync();

        var response = await BloggingApiClient.DeleteUserAsync(_user.CNP);

        if (!response.Success)
        {
            await SnackbarState.PushAsync(response.ResponseMessage, !response.Success);
            await LoadingState.HideAsync();
            return;
        }

        await LogoutAsync();

        await LoadingState.HideAsync();

        await SnackbarState.PushAsync("Successfully deleted!");

        NavigationManager.NavigateTo("/login");
    }
}