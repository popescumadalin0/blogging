using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BloggingClient.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using SDK.Interfaces;

namespace BloggingClient.Pages.Account;

public partial class Account : ComponentBase, IDisposable
{
    [Parameter]
    public string Username { get; set; }

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

    private User _user = new();

    private List<global::Models.Blog> _blogs = new();

    private bool _isMe = false;

    private string _oldUsername = string.Empty;

    public void Dispose()
    {
        SnackbarState.OnStateChange -= StateHasChanged;
        LoadingState.OnStateChange -= StateHasChanged;
    }

    protected override async Task OnInitializedAsync()
    {
        SnackbarState.OnStateChange += StateHasChanged;
        LoadingState.OnStateChange += StateHasChanged;
    }

    protected override async Task OnParametersSetAsync()
    {
        if (_oldUsername != Username)
        {
            _oldUsername = Username;
            await LoadingState.ShowAsync();

            var authState = await AuthenticationState;

            _isMe = Username == authState.User.Claims.First(uc => uc.Type == ClaimTypes.Name).Value;

            var user = await BloggingApiClient.GetUserByUsernameAsync(Username);

            if (!user.Success)
            {
                await SnackbarState.PushAsync(user.ResponseMessage, !user.Success);
                await LoadingState.HideAsync();
                return;
            }

            _user = user.Response;

            var blogs = await BloggingApiClient.GetBlogsByUserAsync(_user.Username);
            if (!blogs.Success)
            {
                await SnackbarState.PushAsync(
                    blogs.ResponseMessage,
                    true);
                await LoadingState.HideAsync();

                return;
            }

            _blogs = blogs.Response.Select(
                    b => new global::Models.Blog()
                    {
                        Image = b.Image,
                        Description = b.Description,
                        Title = b.Title,
                        UserName = b.UserName,
                        BlogCategory = b.BlogCategory,
                        CreatedDate = b.CreatedDate,
                        Id = b.Id,
                    })
                .ToList();

            await LoadingState.HideAsync();
        }
    }

    private void UserSettingsClicked()
    {
        NavigationManager.NavigateTo("/user-settings");
    }

    private async Task LogoutAsync()
    {
        await LoadingState.ShowAsync();

        NavigationManager.NavigateTo("/login");

        var customProvider = (BloggingAuthenticationStateProvider)AuthenticationStateProvider;
        await customProvider.LogoutUserAsync();

        await SnackbarState.PushAsync("Successfully logout!");

        await LoadingState.HideAsync();
    }

    private void BlogClicked(Guid id)
    {
        NavigationManager.NavigateTo($"blog/{id.ToString()}");
    }
}