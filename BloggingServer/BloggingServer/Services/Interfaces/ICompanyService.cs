using Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace BloggingServer.Services.Interfaces;

public interface ICompanyService
{
    /// <summary/>
    Task<IList<Company>> GetCompaniesAsync();

    /// <summary/>
    Task<Company> GetCompanyAsync(Guid id);

    /// <summary/>
    Task AddCompanyAsync(Company model);

    /// <summary/>
    Task UpdateCompanyAsync(Company model);

    /// <summary/>
    Task DeleteCompanyAsync(Guid id);
}