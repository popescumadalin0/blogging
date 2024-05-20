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

public class LayoverController : BaseController
{
    private readonly ILayoverService _layoverService;

    public LayoverController(ILayoverService layoverService)
    {
        _layoverService = layoverService;
    }
    [HttpGet]
    public async Task<IActionResult> GetLayoversAsync()
    {
        try
        {
            var result = await _layoverService.GetLayoversAsync();
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Layover>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Layover>>(ex));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLayoverAsync(string id)
    {
        try
        {
            var result = await _layoverService.GetLayoverAsync(Guid.Parse(id));

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<Layover>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<Layover>(ex));
        }
    }

    [HttpPost]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> CreateLayoverAsync(Layover layover)
    {
        try
        {
            await _layoverService.AddLayoverAsync(layover);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpPut]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> UpdateLayoverAsync(Layover layover)
    {
        try
        {
            await _layoverService.UpdateLayoverAsync(layover);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> DeleteLayoverAsync(string id)
    {
        try
        {
            await _layoverService.DeleteLayoverAsync(Guid.Parse(id));
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
}