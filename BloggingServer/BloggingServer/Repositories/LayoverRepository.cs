using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using DataBaseLayout.DbContext;

namespace BloggingServer.Repositories;

public class LayoverRepository : ILayoverRepository
{
    private readonly IContext _context;

    public LayoverRepository(IContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IList<Layover>> GetLayoversAsync()
    {
        var layover = await _context.Layovers.ToListAsync();
        return layover;
    }

    /// <inheritdoc />
    public async Task<Layover> GetLayoverAsync(Guid id)
    {
        var layover = await _context.Layovers.FindAsync(id);

        return layover;
    }

    /// <inheritdoc />
    public async Task AddLayoverAsync(Layover model)
    {
        await _context.Layovers.AddAsync(model);

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateLayoverAsync(Layover model)//todo:modifier
    {
        await _context.Layovers.FindAsync(model);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteLayoverAsync(Guid id)
    {
        var layover = await _context.Layovers.SingleAsync(scp => scp.Id == id);
        _context.Layovers.Remove(layover);

        await _context.SaveChangesAsync();
    }
}