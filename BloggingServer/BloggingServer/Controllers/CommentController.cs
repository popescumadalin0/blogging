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

public class CommentController : BaseController
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    [Authorize(Roles.User)]
    public async Task<IActionResult> GetCommentsAsync()
    {
        try
        {
            var result = await _commentService.GetCommentsAsync();
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Comment>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Comment>>(ex));
        }
    }

    [HttpGet("blog/{id}")]
    [Authorize(Roles.User)]
    public async Task<IActionResult> GetCommentsByBlogAsync(string id)
    {
        try
        {
            var result = await _commentService.GetCommentsByBlogAsync(Guid.Parse(id));
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Comment>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<Comment>>(ex));
        }
    }

    [HttpPost]
    [Authorize(Roles.User)]
    public async Task<IActionResult> CreateCommentAsync(AddComment comment)
    {
        try
        {
            await _commentService.AddCommentAsync(comment);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles.User)]
    public async Task<IActionResult> DeleteCommentAsync(string id)
    {
        try
        {
            await _commentService.DeleteCommentAsync(Guid.Parse(id));

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
}