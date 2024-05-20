using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using DataBaseLayout.DbContext;
using DataBaseLayout.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingServer.Repositories;

public class PlaneFacilityRepository : IPlaneFacilityRepository
{
    private readonly IContext _context;

    public PlaneFacilityRepository(IContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IList<PlaneFacility>> GetPlaneFacilitiesAsync()
    {
        var planeFacility = await _context.PlaneFacilities.ToListAsync();
        return planeFacility;

    }

    /// <inheritdoc />
    public async Task<PlaneFacility> GetPlaneFacilityAsync(Guid id)
    {
        var planeFacility = await _context.PlaneFacilities.FindAsync(id);

        return planeFacility;
    }

    /// <inheritdoc />
    public async Task AddPlaneFacilityAsync(PlaneFacility model)
    {
        await _context.PlaneFacilities.AddAsync(model);

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdatePlaneFacilityAsync(PlaneFacility model)
    {
        await _context.PlaneFacilities.FindAsync(model);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeletePlaneFacilityAsync(Guid id)
    {
        var planeFacility = await _context.PlaneFacilities.SingleAsync(scp => scp.Id == id);
        _context.PlaneFacilities.Remove(planeFacility);

        await _context.SaveChangesAsync();
    }
}