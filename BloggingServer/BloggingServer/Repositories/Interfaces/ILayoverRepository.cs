using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseLayout.Models;

namespace BloggingServer.Repositories.Interfaces;

public interface ILayoverRepository
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