using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.ResponseModels;
using BloggingServer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Constants;

namespace BloggingServer.Controllers;

public class BlogController : BaseController
{
    private readonly IBlogService _blogService;

    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpPost("filter")]
    [Authorize(Roles.User)]
    public async Task<IActionResult> GetBlogsAsync(BlogFilter filter = null)
    {
        try
        {
            var result = await _blogService.GetBlogsAsync(filter);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Blog>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Blog>>(ex));
        }
    }

    [HttpGet("username/{username}")]

    [Authorize(Roles.User)]
    public async Task<IActionResult> GetBlogsByUserAsync(string username)
    {
        try
        {
            var result = await _blogService.GetBlogsByUserAsync(username);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Blog>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Blog>>(ex));
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles.User)]
    public async Task<IActionResult> GetBlogAsync(string id)
    {
        try
        {
            var result = await _blogService.GetBlogAsync(Guid.Parse(id));

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<Blog>(result));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<Blog>(ex));
        }
    }

    [HttpPost]
    [Authorize(Roles.User)]
    public async Task<IActionResult> CreateBlogAsync(AddBlog blog)
    {
        try
        {
            await _blogService.AddBlogAsync(blog);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles.User)]
    public async Task<IActionResult> DeleteBlogAsync(string id)
    {
        try
        {
            await _blogService.DeleteBlogAsync(Guid.Parse(id));

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
}