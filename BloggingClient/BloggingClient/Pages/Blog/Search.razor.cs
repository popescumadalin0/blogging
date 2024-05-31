using Blazorise;
using BloggingClient.Models;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using BloggingClient.States;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SDK.Interfaces;
using Models;

namespace BloggingClient.Pages.Blog;

public partial class Search : ComponentBase, IDisposable
{
    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    [Inject]
    private IBloggingApiClient BloggingApiClient { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationState { get; set; }

    private List<string> _blogCategories = new();

    private List<global::Models.Blog> _blogs = new();

    public void Dispose()
    {
        SnackbarState.OnStateChange -= StateHasChanged;
        LoadingState.OnStateChange -= StateHasChanged;
    }

    protected override async Task OnInitializedAsync()
    {
        SnackbarState.OnStateChange += StateHasChanged;
        LoadingState.OnStateChange += StateHasChanged;

        await GetBlogCategoriesAsync();

        await GetBlogsAsync();
    }

    private async Task GetBlogCategoriesAsync()
    {
        await LoadingState.ShowAsync();
        var categories = await BloggingApiClient.GetBlogCategoriesAsync();
        if (!categories.Success)
        {
            await SnackbarState.PushAsync(
                categories.ResponseMessage,
                true);
            await LoadingState.HideAsync();

            return;
        }

        _blogCategories = categories.Response.Select(bc => bc.Name).ToList();
        await LoadingState.HideAsync();
    }

    private async Task GetBlogsAsync()
    {
        await LoadingState.ShowAsync();
        var blogs = await BloggingApiClient.GetBlogsAsync();
        if (!blogs.Success)
        {
            await SnackbarState.PushAsync(
                blogs.ResponseMessage,
                true);
            await LoadingState.HideAsync();

            return;
        }

        _blogs = blogs.Response.Select(b => new global::Models.Blog()
        {
            Image = b.Image,
            Description = b.Description,
            Title = b.Title,
            UserName = b.UserName,
            UserId = b.UserId,
            BlogCategory = b.BlogCategory,
            CreatedDate = b.CreatedDate,
            Id = b.Id,
        }).ToList();
        await LoadingState.HideAsync();
    }

    private async Task BlogClickedAsync(Guid id)
    {

    }
}