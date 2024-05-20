using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace BloggingServer.Services.Interfaces;

public interface ILayoverService
{
    /// <summary/>
    Task<IList<Layover>> GetLayoversAsync();

    /// <summary/>
    Task<Layover> GetLayoverAsync(Guid id);

    /// <summary/>
    Task AddLayoverAsync(Layover model);

    /// <summary/>
    Task UpdateLayoverAsync(Layover model);

    /// <summary/>
    Task DeleteLayoverAsync(Guid id);
}