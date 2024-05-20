using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Microsoft.AspNetCore.Components;
using SDK.Interfaces;

namespace BloggingClient.Pages.Flights;

public partial class Explore : ComponentBase
{
    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private IBloggingApiClient BloggingApiClient { get; set; }

    private FlightFilter _filter = new();

    private List<FlightModel> flights = new();

    private List<FlightModel> _filteredFlights = new();

    private List<LocationModel> FromLocations = new();

    private List<LocationModel> ToLocations = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadingState.ShowAsync();

        var tickets = await BloggingApiClient.GetTicketsAsync();

        if (!tickets.Success)
        {
            await SnackbarState.PushAsync(tickets.ResponseMessage, true);
            await LoadingState.HideAsync();

            return;
        }

        FromLocations = tickets.Response.Select(t => new LocationModel()
        {
            City = t.FromCity,
            Country = t.FromCountry,
        }).ToList();
        ToLocations = tickets.Response.Select(t => new LocationModel()
        {
            City = t.ToCity,
            Country = t.ToCountry,
        }).ToList();

        flights = tickets.Response.Select(t => new FlightModel()
        {
            Id = t.Id,
            ArrivalTime = t.ArrivalTime,
            Currency = t.Currency,
            DepartureTime = t.DepartureTime,
            ToLocation = new LocationModel()
            {
                City = t.ToCity,
                Country = t.ToCountry,
            },
            FromLocation = new LocationModel()
            {
                City = t.FromCity,
                Country = t.FromCountry,
            },
            Price = t.Price,
            Image = t.Image
        }).ToList();
        _filteredFlights = flights;
        await LoadingState.HideAsync();
    }

    private Task DepartingChangedAsync(DateTime value)
    {
        _filter.Departing = value;
        //se apleaza iar baza
        //flights = new List<BookingModel>();

        return Task.CompletedTask;
    }
}