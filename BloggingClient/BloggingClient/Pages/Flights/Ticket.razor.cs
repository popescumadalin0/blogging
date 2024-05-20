using System;
using System.Linq;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SDK.Interfaces;

namespace BloggingClient.Pages.Flights;

public partial class Ticket
{
    [Parameter]
    public string Id { get; set; }

    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private IBloggingApiClient BloggingApiClient { get; set; }

    /*[Inject]
    private Task<AuthenticationState> AuthenticationState { get; set; }*/

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private TicketModel _ticketInfo = new TicketModel();

    protected override async Task OnInitializedAsync()
    {
        await LoadingState.ShowAsync();

        var response = await BloggingApiClient.GetTicketAsync(Guid.Parse(Id));

        if (!response.Success)
        {
            await SnackbarState.PushAsync(response.ResponseMessage, true);
            await LoadingState.HideAsync();
            return;
        }

        var startLayover = response.Response.Layovers.MinBy(l => l.Order);
        var endLayover = response.Response.Layovers.MaxBy(l => l.Order);

        _ticketInfo = new TicketModel()
        {
            ArrivalDuration = endLayover.ArrivalTime,
            CompanyName = startLayover.CompanyName,
            Currency = response.Response.Currency,
            DepartureTime = startLayover.DepartureTime,
            DestinationAirport = endLayover.DestinationAirport,
            DestinationCity = endLayover.DestinationCity,
            DestinationCountry = endLayover.DestinationCountry,
            Id = response.Response.Id,
            Image = response.Response.Image,
            Price = response.Response.Price,
            StartPointAirport = startLayover.StartPointAirport,
            StartPointCity = startLayover.StartPointCity,
            StartPointCountry = startLayover.StartPointCountry,
            Layovers = response.Response.Layovers,
        };

        await LoadingState.HideAsync();
    }

    private void OnClick()
    {
        //ar AuthenticationState
        NavigationManager.NavigateTo("/booking");
    }
}