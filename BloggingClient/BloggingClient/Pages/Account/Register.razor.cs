/*using System;
using System.IO;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Models;
using SDK.Interfaces;

namespace BloggingClient.Pages.Account;

public partial class Register : ComponentBase, IDisposable
{
    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IBloggingApiClient BloggingApiClient { get; set; }

    private RegisterModel _registerModel = new RegisterModel();

    private Form form;

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

    private async Task RegisterAsync()
    {
        await LoadingState.ShowAsync();
        var result = await BloggingApiClient.RegisterUserAsync(
            new UserRegister()
            {
                Password = _registerModel.Password,
                User = new User()
                {
                    CNP = _registerModel.CNP,
                    Email = _registerModel.Email,
                    FirstName = _registerModel.FirstName,
                    LastName = _registerModel.LastName,
                    PhoneNumber = _registerModel.PhoneNumber,
                    UserName = _registerModel.UserName,
                    Document = Convert.ToBase64String(_registerModel.Document),
                    ProfileImage = Convert.ToBase64String(_registerModel.ProfileImage),
                }
            });

        await LoadingState.HideAsync();

        await SnackbarState.PushAsync(
            result.Success ? "User created!" : result.ResponseMessage,
            !result.Success);

        if (result.Success)
        {
            NavigationManager.NavigateTo("/login");
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

    private async Task OnDocumentUploaded(FileUploadEventArgs e)
    {
        try
        {
            using var result = new MemoryStream();
            await e.File.OpenReadStream(long.MaxValue).CopyToAsync(result);

            _registerModel.Document = result.ToArray();
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
}*/