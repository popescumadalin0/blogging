/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.ResponseModels;
using BloggingServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BloggingServer.Controllers;

public class RoleController : BaseController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    [HttpGet]
    public async Task<IActionResult> GetRolesAsync()
    {
        try
        {
            var result = await _roleService.GetRolesAsync();
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Role>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Role>>(ex));
        }
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetRoleAsync(string name)
    {
        try
        {
            var result = await _roleService.GetRoleAsync(name);

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<Role>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<Role>(ex));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync(Role role)
    {
        try
        {
            await _roleService.CreateRoleAsync(role);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRoleAsync(Role role)
    {
        try
        {
            await _roleService.UpdateRoleAsync(role);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteRoleAsync(string name)
    {
        try
        {
            await _roleService.DeleteRoleAsync(name);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
}*/