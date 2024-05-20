using System;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Blazorise;
using Microsoft.AspNetCore.Components;
using SDK.Interfaces;

namespace BloggingClient.Pages.Flights;

public partial class Flight : BaseComponent
{
    [Parameter]
    public FlightModel FlightInfo { get; set; }

    [Inject]
    private IBloggingApiClient BloggingApiClient { get; set; }

    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private void OnClick()
    {
        NavigationManager.NavigateTo($"ticket/{FlightInfo.Id.ToString()}");
    }

    private async Task OnDeleteAsync()
    {
        await LoadingState.ShowAsync();

        var response = await BloggingApiClient.DeleteTicketAsync(FlightInfo.Id);

        if (!response.Success)
        {
            await SnackbarState.PushAsync(response.ResponseMessage, true);
            await LoadingState.HideAsync();
            return;
        }

        await SnackbarState.PushAsync("Successfully deleted!");
        await LoadingState.HideAsync();
    }
}
