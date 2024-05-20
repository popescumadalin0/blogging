using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.ResponseModels;
using BloggingServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Constants;

namespace BloggingServer.Controllers;

public class PlaneFacilityController : BaseController
{
    private readonly IPlaneFacilityService _planeFacilityService;

    public PlaneFacilityController(IPlaneFacilityService planeFacilityService)
    {
        _planeFacilityService = planeFacilityService;
    }
    [HttpGet]
    public async Task<IActionResult> GetPlaneFacilitiesAsync()
    {
        try
        {
            var result = await _planeFacilityService.GetPlaneFacilitiesAsync();
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<PlaneFacility>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<PlaneFacility>>(ex));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlaneFacilityAsync(string id)
    {
        try
        {
            var result = await _planeFacilityService.GetPlaneFacilityAsync(Guid.Parse(id));

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<PlaneFacility>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<PlaneFacility>(ex));
        }
    }

    [HttpPost]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> CreatePlaneFacilityAsync(PlaneFacility planeFacility)
    {
        try
        {
            await _planeFacilityService.AddPlaneFacilityAsync(planeFacility);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpPut]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> UpdatePlaneFacilityAsync(PlaneFacility planeFacility)
    {
        try
        {
            await _planeFacilityService.AddPlaneFacilityAsync(planeFacility);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> DeletePlaneFacilityAsync(string id)
    {
        try
        {
            await _planeFacilityService.DeletePlaneFacilityAsync(Guid.Parse(id));
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
}