using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingClient.Models;
using BloggingClient.States;
using Blazorise;
using Microsoft.AspNetCore.Components;
using SDK.Clients;
using SDK.Interfaces;

namespace BloggingClient.Pages.Booking
{
    public partial class BookingHistory : ComponentBase, IDisposable
    {
        [Inject]
        private SnackbarState SnackbarState { get; set; }

        [Inject]
        private LoadingState LoadingState { get; set; }

        [Inject]
        private IBloggingApiClient BloggingApiClient { get; set; }

        private List<FlightModel> flights = new();

        public void Dispose()
        {
            SnackbarState.OnStateChange -= StateHasChanged;
            LoadingState.OnStateChange -= StateHasChanged;
        }

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
            await LoadingState.HideAsync();
        }
    }
}
