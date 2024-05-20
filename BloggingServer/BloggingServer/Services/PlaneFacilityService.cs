using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;
using PlaneFacility = Models.PlaneFacility;


namespace BloggingServer.Services;

public class PlaneFacilityService : IPlaneFacilityService
{
    private readonly IPlaneFacilityRepository _planeFacilityRepository;

    public PlaneFacilityService(IPlaneFacilityRepository planeFacilityRepository)
    {
        _planeFacilityRepository = planeFacilityRepository;
    }

    /// <inheritdoc />
    public async Task<IList<PlaneFacility>> GetPlaneFacilitiesAsync()
    {
        var planeFacility = await _planeFacilityRepository.GetPlaneFacilitiesAsync();
        var response = planeFacility.Select(pf => new PlaneFacility
        {
            Id = pf.Id,
            Name = pf.Name
        }).ToList();
        return response;
    }

    /// <inheritdoc />
    public async Task<PlaneFacility> GetPlaneFacilityAsync(Guid id)
    {
        var planeFacility = await _planeFacilityRepository.GetPlaneFacilityAsync(id);

        var response = new PlaneFacility
        {
            Id = planeFacility.Id,
            Name = planeFacility.Name
        };

        return response;
    }

    /// <inheritdoc />
    public async Task AddPlaneFacilityAsync(PlaneFacility model)
    {
        var entity = new DataBaseLayout.Models.PlaneFacility
        {
            Id = model.Id,
            Name = model.Name
        };
        await _planeFacilityRepository.AddPlaneFacilityAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdatePlaneFacilityAsync(PlaneFacility model)
    {
        var entity = new DataBaseLayout.Models.PlaneFacility
        {
            Id = model.Id,
            Name = model.Name
        };
        await _planeFacilityRepository.UpdatePlaneFacilityAsync(entity);
    }

    /// <inheritdoc />
    public async Task DeletePlaneFacilityAsync(Guid id)
    {
        await _planeFacilityRepository.DeletePlaneFacilityAsync(id);
    }
}