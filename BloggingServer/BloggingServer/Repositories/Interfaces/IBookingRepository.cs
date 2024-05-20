using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLayout.Models;

namespace BloggingServer.Repositories.Interfaces;

public interface IBookingRepository
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