using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazorise;
using BloggingClient.Models;
using BloggingClient.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using SDK.Interfaces;

namespace BloggingClient.Pages.Blog;

public partial class Write : ComponentBase, IDisposable
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

    private AddBlogModel _addBlogModel = new();

    private Validations _validations;

    private List<string> _blogCategories = new();

    private string _newBlogCategory = string.Empty;

    private Modal modalRef;

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
    private async Task OnImageUploaded(FileUploadEventArgs e)
    {
        try
        {
            using var result = new MemoryStream();
            await e.File.OpenReadStream(long.MaxValue).CopyToAsync(result);

            _addBlogModel.Image = result.ToArray();
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

    private async Task AddBlogAsync()
    {
        if (await _validations.ValidateAll())
        {
            await LoadingState.ShowAsync();
            var authState = await AuthenticationState;
            var result = await BloggingApiClient.CreateBlogAsync(
                new AddBlog()
                {
                    Description = _addBlogModel.Description,
                    BlogCategoryName = _addBlogModel.BlogCategoryName,
                    Image = _addBlogModel.Image != null ? Convert.ToBase64String(_addBlogModel.Image) : string.Empty,
                    Title = _addBlogModel.Title,
                    Username = authState.User.Claims.First(c => c.Type == ClaimTypes.Name).Value,
                });

            await LoadingState.HideAsync();

            await SnackbarState.PushAsync(
                result.Success ? "Blog created!" : result.ResponseMessage,
                !result.Success);

            if (result.Success)
            {
                NavigationManager.NavigateTo("/search");
            }
        }
    }

    private async Task AddBlogCategoryAsync()
    {
        await LoadingState.ShowAsync();
        var result = await BloggingApiClient.CreateBlogCategoryAsync(
            new BlogCategory()
            {
                Name = _newBlogCategory
            });

        await LoadingState.HideAsync();

        await SnackbarState.PushAsync(
            result.Success ? "Blog category created!" : result.ResponseMessage,
            !result.Success);

        if (result.Success)
        {
            _newBlogCategory = string.Empty;
            await HideModalAsync();
        }

        await GetBlogCategoriesAsync();
    }

    private Task ShowModalAsync()
    {
        return modalRef.Show();
    }

    private Task HideModalAsync()
    {
        return modalRef.Hide();
    }
}