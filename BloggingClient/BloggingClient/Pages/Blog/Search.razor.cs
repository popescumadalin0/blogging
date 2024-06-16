using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using SDK.Interfaces;

namespace BloggingClient.Pages.Blog;

public partial class Search : ComponentBase, IDisposable
{
    [Parameter]
    public string Filter { get; set; }

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

    private List<FilterCheck> _blogCategories = new();

    private List<global::Models.Blog> _blogs = new();

    private string _oldFilter = string.Empty;

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
        if (_oldFilter != Filter)
        {
            _oldFilter = Filter;

            await LoadingState.ShowAsync();

            await GetBlogCategoriesAsync();

            await GetBlogsAsync();

            await LoadingState.HideAsync();
        }
    }

    private async Task GetBlogCategoriesAsync()
    {
        var categories = await BloggingApiClient.GetUsedBlogCategoriesAsync();
        if (!categories.Success)
        {
            await SnackbarState.PushAsync(
                categories.ResponseMessage,
                true);
            return;
        }

        _blogCategories = categories.Response.Select(bc => new FilterCheck(bc.Name)).ToList();
    }

    private async Task FilterBlogsAsync()
    {
        await LoadingState.ShowAsync();

        await GetBlogsAsync();

        await LoadingState.HideAsync();
    }

    private async Task GetBlogsAsync()
    {
        var blogs = await BloggingApiClient.GetBlogsAsync(new BlogFilter()
        {
            FilterValue = Filter,
            BlogCategories = _blogCategories.Where(bc => bc.IsChecked).Select(bc => bc.Name).ToList()
        });

        if (!blogs.Success)
        {
            await SnackbarState.PushAsync(
                blogs.ResponseMessage,
                true);

            return;
        }

        _blogs = blogs.Response.Select(b => new global::Models.Blog()
        {
            Image = b.Image,
            Description = b.Description,
            Title = b.Title,
            UserName = b.UserName,
            BlogCategory = b.BlogCategory,
            CreatedDate = b.CreatedDate,
            Id = b.Id,
        }).ToList();
    }

    private void BlogClicked(Guid id)
    {
        NavigationManager.NavigateTo($"blog/{id.ToString()}");
    }
}
