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

public class PlaneSeatController : BaseController
{
    private readonly IPlaneSeatService _planeSeatService;

    public PlaneSeatController(IPlaneSeatService planeSeatService)
    {
        _planeSeatService = planeSeatService;
    }
    [HttpGet]
    public async Task<IActionResult> GetPlaneSeatsAsync()
    {
        try
        {
            var result = await _planeSeatService.GetPlaneSeatsAsync();
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<PlaneSeat>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<PlaneSeat>>(ex));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlaneSeatAsync(string id)
    {
        try
        {
            var result = await _planeSeatService.GetPlaneSeatAsync(Guid.Parse(id));

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<PlaneSeat>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<PlaneSeat>(ex));
        }
    }

    [HttpPost]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> CreatePlaneSeatAsync(PlaneSeat planeSeat)
    {
        try
        {
            await _planeSeatService.AddPlaneSeatAsync(planeSeat);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpPut]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> UpdatePlaneSeatAsync(PlaneSeat planeSeat)
    {
        try
        {
            await _planeSeatService.UpdatePlaneSeatAsync(planeSeat);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> DeletePlaneSeatAsync(string id)
    {
        try
        {
            await _planeSeatService.DeletePlaneSeatAsync(Guid.Parse(id));
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
}