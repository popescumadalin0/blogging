using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace BloggingServer.Services.Interfaces;

public interface IPlaneSeatService
{
    /// <summary/>
    Task<IList<PlaneSeat>> GetPlaneSeatsAsync();

    /// <summary/>
    Task<PlaneSeat> GetPlaneSeatAsync(Guid id);

    /// <summary/>
    Task AddPlaneSeatAsync(PlaneSeat model);

    /// <summary/>
    Task UpdatePlaneSeatAsync(PlaneSeat model);

    /// <summary/>
    Task DeletePlaneSeatAsync(Guid id);
}