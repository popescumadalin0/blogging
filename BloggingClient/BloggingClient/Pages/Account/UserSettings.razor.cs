using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Models;
using SDK.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BloggingClient.Pages.Account;

public partial class UserSettings : ComponentBase, IDisposable
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

    private EditUserModel _editModel = new();

    private Validations _validations;

    private FilePicker _filePicker = new FilePicker();

    private byte[] _profileImage = null;

    private bool _isSaveClickable = false;

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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadingState.ShowAsync();

            await GetUserAsync();

            await LoadingState.HideAsync();
        }
    }

    private async Task SaveChangesAsync()
    {
        var canSave = false;

        if (string.IsNullOrEmpty(_editModel.ConfirmNewPassword) &&
             string.IsNullOrEmpty(_editModel.NewPassword) &&
             string.IsNullOrEmpty(_editModel.OldPassword))
        {
            canSave = true;
        }
        else if (await _validations.ValidateAll())
        {
            canSave = true;
        }

        if (canSave)
        {
            await LoadingState.ShowAsync();

            var authState = await AuthenticationState;
            var result = await BloggingApiClient.UpdateUserAsync(
                new UpdateUser()
                {
                    OldPassword = _editModel.OldPassword,
                    NewPassword = _editModel.NewPassword,
                    ProfileImage = _editModel.ProfileImage != null ? Convert.ToBase64String(_editModel.ProfileImage) : string.Empty,
                    Username = authState.User.Claims.First(c => c.Type == ClaimTypes.Name).Value,
                });

            if (result.Success)
            {
                _isSaveClickable = false;
                await GetUserAsync();
            }

            await LoadingState.HideAsync();

            await SnackbarState.PushAsync(
                result.Success ? "User updated!" : result.ResponseMessage,
                !result.Success);
        }
    }

    private async Task GetUserAsync()
    {
        var authState = await AuthenticationState;

        var username = authState.User.Claims.First(uc => uc.Type == ClaimTypes.Name).Value;

        var user = await BloggingApiClient.GetUserByUsernameAsync(username);

        if (!user.Success)
        {
            await SnackbarState.PushAsync(user.ResponseMessage, !user.Success);
            await LoadingState.HideAsync();
            return;
        }

        await _filePicker.Clear();

        _profileImage = Convert.FromBase64String(user.Response.ProfileImage);

        _editModel = new EditUserModel();
    }

    private async Task OnImageUploaded(FileUploadEventArgs e)
    {
        try
        {
            using var result = new MemoryStream();
            await e.File.OpenReadStream(long.MaxValue).CopyToAsync(result);

            _editModel.ProfileImage = result.ToArray();
            _isSaveClickable = true;
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