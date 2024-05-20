using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.Request;

namespace BloggingServer.Services.Interfaces;

public interface ITicketService
{
    /// <summary/>
    Task<IList<Ticket>> GetTicketsAsync();

    /// <summary/>
    Task<TicketDetail> GetTicketAsync(Guid id);

    /// <summary/>
    Task CreateTicketAsync(AddTicket ticket);

    /// <summary/>
    Task UpdateTicketAsync(Ticket ticket);

    /// <summary/>
    Task DeleteTicketAsync(Guid id);
}