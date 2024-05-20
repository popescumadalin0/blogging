using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using DataBaseLayout.DbContext;
using DataBaseLayout.Models;
using Microsoft.EntityFrameworkCore;


namespace BloggingServer.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly IContext _context;

    public CompanyRepository(IContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IList<Company>> GetCompaniesAsync()
    {
        var company = await _context.Companies.ToListAsync();
        return company;

    }

    /// <inheritdoc />
    public async Task<Company> GetCompanyAsync(Guid id)
    {
        var company = await _context.Companies.FindAsync(id);

        return company;
    }

    /// <inheritdoc />
    public async Task AddCompanyAsync(Company model)
    {
        await _context.Companies.AddAsync(model);

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateCompanyAsync(Company model)
    {
        await _context.Companies.FindAsync(model);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteCompanyAsync(Guid id)
    {
        var company = await _context.Companies.SingleAsync(scp => scp.Id == id);
        _context.Companies.Remove(company);

        await _context.SaveChangesAsync();
    }
}
