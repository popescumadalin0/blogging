using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.ResponseModels;
using BloggingServer.Services.Interfaces;
using DataBaseLayout.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Constants;

namespace BloggingServer.Controllers;

public class BlogController : BaseController
{
    private readonly IRepositoryBase<Blog> _blogRepository;

    public BlogController(IRepositoryBase<Blog> blogRepository)
    {
        _blogRepository = blogRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTicketsAsync()
    {
        try
        {
            var result = await _blogRepository.GetAllAsync();
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Blog>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Blog>>(ex));
        }
    }

    /*
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketAsync(string id)
    {
        try
        {
            var result = await _ticketService.GetTicketAsync(Guid.Parse(id));

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<TicketDetail>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<TicketDetail>(ex));
        }
    }

    [HttpPost]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> CreateTicketAsync(AddTicket ticket)
    {
        try
        {
            await _ticketService.CreateTicketAsync(ticket);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpPut]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> UpdateTicketAsync(Ticket ticket)
    {
        try
        {
            await _ticketService.UpdateTicketAsync(ticket);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles.Employee)]
    public async Task<IActionResult> DeleteTicketAsync(string id)
    {
        try
        {
            await _ticketService.DeleteTicketAsync(Guid.Parse(id));
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
    */

}