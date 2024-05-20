using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingServer.Repositories.Interfaces;

public interface IPlaneSeatRepository
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