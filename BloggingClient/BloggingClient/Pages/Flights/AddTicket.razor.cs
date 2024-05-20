using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Models.Request;
using SDK.Interfaces;

namespace BloggingClient.Pages.Flights;

public partial class AddTicket : ComponentBase, IDisposable
{
    [Inject]
    private SnackbarState SnackbarState { get; set; }

    [Inject]
    private LoadingState LoadingState { get; set; }

    [Inject]
    private IBloggingApiClient BloggingApiClient { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private AddTicketModel _ticket = new();

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

    private async Task AddTicketAsync()
    {
        await LoadingState.ShowAsync();

        var response = await BloggingApiClient.CreateTicketAsync(
            new global::Models.Request.AddTicket()
            {
                Currency = _ticket.Currency,
                Image = _ticket.Image,
                Price = _ticket.Price,
                Layovers = _ticket.Layovers.Select(
                        l => new AddLayover()
                        {
                            ArrivalDuration = l.ArrivalDuration,
                            CompanyName = l.CompanyName,
                            PlaneFacilities = l.PlaneFacilities.Select(
                                    pf => new AddPlaneFacility()
                                    {
                                        Name = pf.Name,
                                    })
                                .ToList(),
                            PlaneSeats = l.PlaneSeats,
                            DepartureTime = l.DepartureTime,
                            DestinationAirport = l.DestinationAirport,
                            DestinationCity = l.DestinationCity,
                            DestinationCountry = l.DestinationCountry,
                            Order = l.Order,
                            StartPointAirport = l.StartPointAirport,
                            StartPointCity = l.StartPointCity,
                            StartPointCountry = l.StartPointCountry,
                            Id = l.Id,
                        })
                    .ToList()
            });

        if (!response.Success)
        {
            await SnackbarState.PushAsync(response.ResponseMessage, true);
            await LoadingState.HideAsync();
            return;
        }

        await SnackbarState.PushAsync("Successfully added!");
        await LoadingState.HideAsync();

        NavigationManager.NavigateTo("/explore");
    }

    private async Task OnImageUploaded(FileUploadEventArgs e)
    {
        try
        {
            using var result = new MemoryStream();
            await e.File.OpenReadStream(long.MaxValue).CopyToAsync(result);

            _ticket.Image = Convert.ToBase64String(result.ToArray());
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