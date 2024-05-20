using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using DataBaseLayout.DbContext;

namespace BloggingServer.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly IContext _context;

    public BookingRepository(IContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IList<Booking>> GetBookingsAsync()
    {
        var bookings = await _context.Bookings.ToListAsync();

        return bookings;
    }

    /// <inheritdoc />
    public async Task<Booking> GetBookingAsync(Guid id)
    {
        var booking = await _context.Bookings.FindAsync(id);

        return booking;
    }

    /// <inheritdoc />
    public async Task AddBookingAsync(Booking model)
    {
        await _context.Bookings.AddAsync(model);

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateBookingAsync(Booking model)//todo:modified
    {
        await _context.Bookings.FindAsync(model);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteBookingAsync(Guid id)
    {
        var booking = await _context.Bookings.SingleAsync(scp => scp.Id == id);
        _context.Bookings.Remove(booking);

        await _context.SaveChangesAsync();
    }
}