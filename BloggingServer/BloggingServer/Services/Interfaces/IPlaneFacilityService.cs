using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace BloggingServer.Services.Interfaces;

public interface IPlaneFacilityService
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