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

namespace BloggingClient.Pages.Blog;

public partial class BlogDetail : ComponentBase, IDisposable
{
    [Parameter]
    public string Id { get; set; }

    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private IBloggingApiClient BloggingApiClient { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationState { get; set; }

    private global::Models.Blog _blog = new();

    private List<Comment> _comments = new();

    private string _comment = string.Empty;

    private string _oldId = string.Empty;

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
        if (Id != _oldId)
        {
            _oldId = Id;
            await LoadingState.ShowAsync();

            var blog = await BloggingApiClient.GetBlogAsync(Id);
            if (!blog.Success)
            {
                await SnackbarState.PushAsync(
                    blog.ResponseMessage,
                    true);
                await LoadingState.HideAsync();

                return;
            }

            _blog = blog.Response;

            await GetCommentsAsync();

            await LoadingState.HideAsync();
        }
    }

    private async Task GetCommentsAsync()
    {
        var comments = await BloggingApiClient.GetCommentsByBlogAsync(Id);

        if (!comments.Success)
        {
            await SnackbarState.PushAsync(
                comments.ResponseMessage,
                true);
            await LoadingState.HideAsync();

            return;
        }

        _comments = comments.Response.OrderByDescending(c => c.CreatedDate).ToList();
    }

    private async Task AddCommentAsync()
    {
        if (string.IsNullOrEmpty(_comment))
        {
            return;
        }

        await LoadingState.ShowAsync();

        var authState = await AuthenticationState;
        var username = authState.User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        var response = await BloggingApiClient.CreateCommentAsync(
            new AddComment()
            {
                Username = username,
                BlogId = _blog.Id,
                Description = _comment
            });

        await SnackbarState.PushAsync(
            response.Success ? "Comment added!" : response.ResponseMessage,
            !response.Success);

        await GetCommentsAsync();

        await LoadingState.HideAsync();
    }
}
