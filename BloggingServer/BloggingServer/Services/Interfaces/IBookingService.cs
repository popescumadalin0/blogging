using Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace BloggingServer.Services.Interfaces;

public interface IBookingService
{
    /// <summary/>
    Task<IList<Booking>> GetBookingsAsync();

    /// <summary/>
    Task<Booking> GetBookingAsync(Guid id);

    /// <summary/>
    Task AddBookingAsync(Booking model);

    /// <summary/>
    Task UpdateBookingAsync(Booking model);

    /// <summary/>
    Task DeleteBookingAsync(Guid id);
}