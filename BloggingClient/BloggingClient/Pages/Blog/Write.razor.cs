using Blazorise;
using BloggingClient.Models;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using BloggingClient.States;
using SDK.Interfaces;
using Models;

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

    private AddBlogModel _addBlogModel = new();

    private Validations _validations;

    private List<string> _blogCategories = new();

    protected override async Task OnInitializedAsync()
    {
        SnackbarState.OnStateChange += StateHasChanged;
        LoadingState.OnStateChange += StateHasChanged;

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

    public void Dispose()
    {
        SnackbarState.OnStateChange -= StateHasChanged;
        LoadingState.OnStateChange -= StateHasChanged;
    }

    private async Task AddBlogAsync()
    {
        if (await _validations.ValidateAll())
        {
            await LoadingState.ShowAsync();
            var result = await BloggingApiClient.CreateBlogAsync(
                new AddBlog()
                {
                    Description = _addBlogModel.Description,
                    BlogCategoryName = _addBlogModel.BlogCategoryName,
                    Image = _addBlogModel.Image != null ? Convert.ToBase64String(_addBlogModel.Image) : string.Empty,
                    Title = _addBlogModel.Title,
                    UserId = ""//todo
                });

            await LoadingState.HideAsync();

            await SnackbarState.PushAsync(
                result.Success ? "Blog created!" : result.ResponseMessage,
                !result.Success);

            if (result.Success)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}