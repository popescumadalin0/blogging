using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;

namespace BloggingServer.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    /// <inheritdoc />
    public async Task<IList<Company>> GetCompaniesAsync()
    {
        var company = await _companyRepository.GetCompaniesAsync();

        var response = company.Select(c => new Company
        {
            Id = c.Id,
            Name = c.Name,
            Raiting = c.Raiting
        }).ToList();

        return response;
    }

    /// <inheritdoc />
    public async Task<Company> GetCompanyAsync(Guid id)
    {
        var company = await _companyRepository.GetCompanyAsync(id);

        var response = new Company
        {
            Id = company.Id,
            Name = company.Name,
            Raiting = company.Raiting
        };

        return response;
    }

    /// <inheritdoc />
    public async Task AddCompanyAsync(Company model)
    {
        var entity = new DataBaseLayout.Models.Company
        {
            Id = model.Id,
            Name = model.Name,
            Raiting = model.Raiting
        };
        await _companyRepository.AddCompanyAsync(entity);
    }

    /// <inheritdoc />
    public async Task UpdateCompanyAsync(Company model)
    {
        var entity = new DataBaseLayout.Models.Company
        {
            Id = model.Id,
            Name = model.Name,
            Raiting = model.Raiting
            //todo: sa vedem daca le adaugam de aici
            //Layovers = 
        };
        await _companyRepository.UpdateCompanyAsync(entity);
    }

    /// <inheritdoc />
    public async Task DeleteCompanyAsync(Guid id)
    {
        await _companyRepository.DeleteCompanyAsync(id);
    }
}