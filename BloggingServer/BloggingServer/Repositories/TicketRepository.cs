using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using DataBaseLayout.DbContext;
using DataBaseLayout.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingServer.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly IContext _context;

    public TicketRepository(IContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IList<Ticket>> GetTicketsAsync()
    {
        var tickets = await _context.Tickets.ToListAsync();
        return tickets;

    }

    /// <inheritdoc />
    public async Task<Ticket> GetTicketAsync(Guid id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        return ticket;
    }

    /// <inheritdoc />
    public async Task AddTicketAsync(Ticket model)
    {
        await _context.Tickets.AddAsync(model);

        await _context.SaveChangesAsync();

    }

    /// <inheritdoc />
    public async Task UpdateTicketAsync(Ticket ticket)//todo: modificat
    {
        await _context.Tickets.FindAsync(ticket);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteTicketAsync(Guid id)
    {
        var airFlighty = await _context.Tickets.SingleAsync(scp => scp.Id == id);
        _context.Tickets.Remove(airFlighty);

        await _context.SaveChangesAsync();
    }
}