using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;

namespace BloggingServer.Services;

public class PlaneSeatService : IPlaneSeatService
{
    private readonly IPlaneSeatRepository _planeSeatRepository;

    public PlaneSeatService(IPlaneSeatRepository planeSeatRepository)
    {
        _planeSeatRepository = planeSeatRepository;
    }

    /// <inheritdoc />
    public async Task<IList<PlaneSeat>> GetPlaneSeatsAsync()
    {
        var planeSeat = await _planeSeatRepository.GetPlaneSeatsAsync();
        var response = planeSeat.Select(ps => new PlaneSeat
        {
            Id = ps.Id,
            IsOcuppied = ps.IsOcuppied
        }).ToList();
        return response;
    }

    /// <inheritdoc />
    public async Task<PlaneSeat> GetPlaneSeatAsync(Guid id)
    {
        var planeSeat = await _planeSeatRepository.GetPlaneSeatAsync(id);
        var response = new PlaneSeat
        {
            Id = planeSeat.Id,
            IsOcuppied = planeSeat.IsOcuppied
        };
        return response;
    }

    /// <inheritdoc />
    public async Task AddPlaneSeatAsync(PlaneSeat model)
    {
        var entity = new DataBaseLayout.Models.PlaneSeat
        {
            Id = model.Id,
            IsOcuppied = model.IsOcuppied
        };
        await _planeSeatRepository.AddPlaneSeatAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdatePlaneSeatAsync(PlaneSeat model)
    {
        var entity = new DataBaseLayout.Models.PlaneSeat
        {
            Id = model.Id,
            IsOcuppied = model.IsOcuppied
        };
        await _planeSeatRepository.UpdatePlaneSeatAsync(entity);
    }

    /// <inheritdoc />
    public async Task DeletePlaneSeatAsync(Guid id)
    {
        await _planeSeatRepository.DeletePlaneSeatAsync(id);
    }
}