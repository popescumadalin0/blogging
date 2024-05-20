using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;
using Booking = DataBaseLayout.Models.Booking;

namespace BloggingServer.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    /// <inheritdoc />
    public async Task<IList<Models.Booking>> GetBookingsAsync()
    {
        var booking = await _bookingRepository.GetBookingsAsync();

        var response = booking.Select(b => new Models.Booking
        {
            Id = b.Id,
        }).ToList();

        return response;
    }

    /// <inheritdoc />
    public async Task<Models.Booking> GetBookingAsync(Guid id)
    {
        var booking = await _bookingRepository.GetBookingAsync(id);

        var response = new Models.Booking
        {
            Id = booking.Id
        };

        return response;
    }

    /// <inheritdoc />
    public async Task AddBookingAsync(Models.Booking model)
    {
        var entity = new Booking
        {
            Id = model.Id
        };
        await _bookingRepository.AddBookingAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdateBookingAsync(Models.Booking model)
    {
        var entity = new Booking
        {
            Id = model.Id
            //todo: sa vedem daca le adaugam de aici
            //Layovers = 
        };
        await _bookingRepository.UpdateBookingAsync(entity);
    }

    /// <inheritdoc />
    public async Task DeleteBookingAsync(Guid id)
    {
        await _bookingRepository.DeleteBookingAsync(id);
    }
}