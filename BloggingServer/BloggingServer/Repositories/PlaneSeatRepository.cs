using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using DataBaseLayout.DbContext;

namespace BloggingServer.Repositories;

public class PlaneSeatRepository : IPlaneSeatRepository
{
    private readonly IContext _context;
    public PlaneSeatRepository(IContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IList<PlaneSeat>> GetPlaneSeatsAsync()
    {
        var planeSeat = await _context.PlaneSeats.ToListAsync();
        return planeSeat;
    }

    /// <inheritdoc />
    public async Task<PlaneSeat> GetPlaneSeatAsync(Guid id)
    {
        var planeSeat = await _context.PlaneSeats.FindAsync(id);

        return planeSeat;
    }

    /// <inheritdoc />
    public async Task AddPlaneSeatAsync(PlaneSeat model)
    {
        await _context.PlaneSeats.AddAsync(model);

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdatePlaneSeatAsync(PlaneSeat model)
    {
        await _context.PlaneSeats.FindAsync(model);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeletePlaneSeatAsync(Guid id)
    {
        var planeSeat = await _context.PlaneSeats.SingleAsync(scp => scp.Id == id);
        _context.PlaneSeats.Remove(planeSeat);

        await _context.SaveChangesAsync();
    }
}