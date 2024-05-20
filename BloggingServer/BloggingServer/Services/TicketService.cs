using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;

namespace BloggingServer.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    /// <inheritdoc />
    public async Task<IList<Models.Ticket>> GetTicketsAsync()
    {
        var tickets = await _ticketRepository.GetTicketsAsync();

        var response = tickets.Select(t =>
        {
            var startLayover = t.Layovers.MaxBy(l => l.Order);
            var endLayover = t.Layovers.MinBy(l => l.Order);

            var response = new Models.Ticket
            {
                Currency = t.Currency,
                Id = t.Id,
                Price = t.Price,
                ArrivalTime = startLayover.ArrivalDuration,
                DepartureTime = endLayover.DepartureTime,
                FromCountry = startLayover.StartPointCountry,
                FromCity = startLayover.StartPointCity,
                NumberOfSeats = t.Layovers.Min(l => l.PlaneSeats.Count),
                ToCity = endLayover.DestinationCity,
                ToCountry = endLayover.DestinationCountry,
                Image = Convert.ToBase64String(t.Image)
            };

            return response;
        }).ToList();

        return response;
    }

    /// <inheritdoc />
    public async Task<Models.TicketDetail> GetTicketAsync(Guid id)
    {
        var ticket = await _ticketRepository.GetTicketAsync(id);

        var response = new Models.TicketDetail
        {
            Currency = ticket.Currency,
            Id = ticket.Id,
            Price = ticket.Price,
            Image = Convert.ToBase64String(ticket.Image),
            Layovers = ticket.Layovers.Select(l => new Models.Layover()
            {
                Order = l.Order,
                ArrivalTime = l.ArrivalDuration,
                DepartureTime = l.DepartureTime,
                DestinationAirport = l.DestinationAirport,
                DestinationCity = l.DestinationCity,
                DestinationCountry = l.DestinationCountry,
                Id = l.Id,
                StartPointAirport = l.StartPointAirport,
                StartPointCity = l.StartPointCity,
                StartPointCountry = l.StartPointCountry,
                CompanyName = l.Company.Name,
                PlaneFacilities = l.PlaneFacilities.Select(pl => new Models.PlaneFacility()
                {
                    Name = pl.Name,
                    Id = pl.Id,
                }).ToList()
            }).ToList(),
            NumberOfSeats = ticket.Layovers.Min(l => l.PlaneSeats.Count),
        };

        return response;
    }

    /// <inheritdoc />
    public async Task CreateTicketAsync(AddTicket ticket)
    {
        var entity = new Ticket
        {
            Currency = ticket.Currency,
            Id = Guid.NewGuid(),
            Price = ticket.Price,
            Image = Convert.FromBase64String(ticket.Image),
            Layovers = ticket.Layovers.Select(l =>
            {
                var planSeats = new List<PlaneSeat>();
                for (var i = 0; i < l.PlaneSeats; i++)
                {
                    planSeats.Add(new PlaneSeat()
                    {
                        Id = Guid.NewGuid(),
                        IsOcuppied = false
                    });
                }
                return new Layover()
                {
                    Order = l.Order,
                    StartPointCity = l.StartPointCity,
                    DestinationCity = l.DestinationCity,
                    StartPointCountry = l.StartPointCountry,
                    DepartureTime = l.DepartureTime,
                    ArrivalDuration = l.ArrivalDuration,
                    DestinationCountry = l.DestinationCountry,
                    Company = new Company()
                    {
                        Name = l.CompanyName,
                        Id = Guid.NewGuid()
                    },
                    Id = Guid.NewGuid(),
                    DestinationAirport = l.DestinationAirport,
                    StartPointAirport = l.StartPointAirport,
                    PlaneFacilities = l.PlaneFacilities.Select(
                            pf => new PlaneFacility()
                            {
                                Name = pf.Name,
                                Id = Guid.NewGuid()
                            })
                        .ToList(),
                    PlaneSeats = planSeats,
                };
            }).ToList(),
        };
        await _ticketRepository.AddTicketAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdateTicketAsync(Models.Ticket ticket)
    {
        var entity = new Ticket
        {
            Currency = ticket.Currency,
            Id = ticket.Id,
            Price = ticket.Price,
            //todo: sa vedem daca le adaugam de aici
            //Layovers = 
        };
        await _ticketRepository.UpdateTicketAsync(entity);
    }

    /// <inheritdoc />
    public async Task DeleteTicketAsync(Guid id)
    {
        await _ticketRepository.DeleteTicketAsync(id);
    }
}