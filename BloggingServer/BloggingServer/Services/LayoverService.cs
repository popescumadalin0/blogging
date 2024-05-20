using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;
using Layover = Models.Layover;

namespace BloggingServer.Services;

public class LayoverService : ILayoverService
{
    private readonly ILayoverRepository _layoverRepository;

    public LayoverService(ILayoverRepository layoverRepository)
    {
        _layoverRepository = layoverRepository;
    }

    /// <inheritdoc />
    public async Task<IList<Layover>> GetLayoversAsync()
    {
        var layover = await _layoverRepository.GetLayoversAsync();

        var response = layover.Select(l => new Layover
        {
            Id = l.Id,
            DepartureTime = l.DepartureTime,
            DestinationAirport = l.DestinationAirport,
            DestinationCity = l.DestinationCity,
            DestinationCountry = l.DestinationCountry,
            ArrivalTime = l.ArrivalDuration,
            StartPointAirport = l.StartPointAirport,
            StartPointCity = l.StartPointCity,
            StartPointCountry = l.StartPointCountry,
            Order = l.Order,
        }).ToList();

        return response;
    }

    /// <inheritdoc />
    public async Task<Layover> GetLayoverAsync(Guid id)
    {
        var layover = await _layoverRepository.GetLayoverAsync(id);

        var response = new Layover
        {
            Id = layover.Id,
            DepartureTime = layover.DepartureTime,
            DestinationAirport = layover.DestinationAirport,
            DestinationCity = layover.DestinationCity,
            DestinationCountry = layover.DestinationCountry,
            ArrivalTime = layover.ArrivalDuration,
            StartPointAirport = layover.StartPointAirport,
            StartPointCity = layover.StartPointCity,
            StartPointCountry = layover.StartPointCountry,
            Order = layover.Order,
        };

        return response;
    }

    /// <inheritdoc />
    public async Task AddLayoverAsync(Layover model)
    {

        var entity = new DataBaseLayout.Models.Layover
        {
            Id = model.Id,
            DepartureTime = model.DepartureTime,
            DestinationAirport = model.DestinationAirport,
            DestinationCity = model.DestinationCity,
            DestinationCountry = model.DestinationCountry,
            ArrivalDuration = model.ArrivalTime,
            StartPointAirport = model.StartPointAirport,
            StartPointCity = model.StartPointCity,
            StartPointCountry = model.StartPointCountry,
            Order = model.Order,
        };

        await _layoverRepository.AddLayoverAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdateLayoverAsync(Layover model)
    {
        var entity = new DataBaseLayout.Models.Layover
        {
            Id = model.Id,
            DepartureTime = model.DepartureTime,
            DestinationAirport = model.DestinationAirport,
            DestinationCity = model.DestinationCity,
            DestinationCountry = model.DestinationCountry,
            ArrivalDuration = model.ArrivalTime,
            StartPointAirport = model.StartPointAirport,
            StartPointCity = model.StartPointCity,
            StartPointCountry = model.StartPointCountry,
        };

        await _layoverRepository.AddLayoverAsync(entity);
    }

    /// <inheritdoc />
    public async Task DeleteLayoverAsync(Guid id)
    {
        await _layoverRepository.DeleteLayoverAsync(id);
    }
}