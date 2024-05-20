using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.ResponseModels;
using BloggingServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Constants;

namespace BloggingServer.Controllers;

public class CompanyController : BaseController
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }
    [HttpGet]

    public async Task<IActionResult> GetCompaniesAsync()
    {
        try
        {
            var result = await _companyService.GetCompaniesAsync();
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Company>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Company>>(ex));
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyAsync(string id)
    {
        try
        {
            var result = await _companyService.GetCompanyAsync(Guid.Parse(id));

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<Company>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<Company>(ex));
        }
    }
    [HttpPost]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> CreateCompanyAsync(Company company)
    {
        try
        {
            await _companyService.AddCompanyAsync(company);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
    [HttpPut]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> UpdateCompanyAsync(Company company)
    {
        try
        {
            await _companyService.UpdateCompanyAsync(company);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> DeleteCompanyAsync(string id)
    {
        try
        {
            await _companyService.DeleteCompanyAsync(Guid.Parse(id));
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
}