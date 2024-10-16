using System;
using System.IO;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Models;
using SDK.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BloggingClient.Pages.Account;

public partial class Register : ComponentBase, IDisposable
{
    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationState { get; set; }

    [Inject]
    private IBloggingApiClient BloggingApiClient { get; set; }

    private RegisterModel _registerModel = new RegisterModel();

    private Validations _validations;

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
            NavigationManager.NavigateTo("/", forceLoad: true);
        }

        SnackbarState.OnStateChange += StateHasChanged;
        LoadingState.OnStateChange += StateHasChanged;
    }

    private async Task RegisterAsync()
    {
        if (await _validations.ValidateAll())
        {
            await LoadingState.ShowAsync();
            var result = await BloggingApiClient.RegisterUserAsync(
                new AddUser()
                {
                    Password = _registerModel.Password,
                    Email = _registerModel.Email,
                    Username = _registerModel.UserName,
                    ProfileImage = _registerModel.ProfileImage != null ? Convert.ToBase64String(_registerModel.ProfileImage) : string.Empty,
                    AcceptTerms = _registerModel.AcceptTerms,
                });

            await LoadingState.HideAsync();

            if (result.Success)
            {
                NavigationManager.NavigateTo("/login");
            }

            await SnackbarState.PushAsync(
                result.Success ? "User created!" : result.ResponseMessage,
                !result.Success);
        }
    }

    private async Task OnImageUploaded(FileUploadEventArgs e)
    {
        try
        {
            using var result = new MemoryStream();
            await e.File.OpenReadStream(long.MaxValue).CopyToAsync(result);

            _registerModel.ProfileImage = result.ToArray();
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
}