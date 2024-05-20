using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLayout.Models;

namespace BloggingServer.Repositories.Interfaces;

public interface IPlaneFacilityRepository
{
    /// <summary/>
    Task<IList<PlaneFacility>> GetPlaneFacilitiesAsync();

    /// <summary/>
    Task<PlaneFacility> GetPlaneFacilityAsync(Guid id);

    /// <summary/>
    Task AddPlaneFacilityAsync(PlaneFacility model);

    /// <summary/>
    Task UpdatePlaneFacilityAsync(PlaneFacility model);

    /// <summary/>
    Task DeletePlaneFacilityAsync(Guid id);
}