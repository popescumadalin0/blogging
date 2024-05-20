using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingServer.Repositories.Interfaces;

public interface ICompanyRepository
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