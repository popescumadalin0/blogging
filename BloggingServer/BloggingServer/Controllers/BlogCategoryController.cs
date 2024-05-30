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

public class BlogCategoryController : BaseController
{
    private readonly IBlogCategoryService _blogCategoryService;

    public BlogCategoryController(IBlogCategoryService blogCategoryService)
    {
        _blogCategoryService = blogCategoryService;
    }

    [HttpGet]
    [Authorize(Roles.User)]
    public async Task<IActionResult> GetBlogCategoriesAsync()
    {
        try
        {
            var result = await _blogCategoryService.GetBlogCategoriesAsync();
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<BlogCategory>>(result.ToList()));

        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse<List<BlogCategory>>(ex));
        }
    }

    [HttpPost]
    [Authorize(Roles.Admin)]
    public async Task<IActionResult> CreateBlogCategoryAsync(BlogCategory blogCategory)
    {
        try
        {
            await _blogCategoryService.CreateBlogCategoryAsync(blogCategory);
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }

    [HttpDelete("{name}")]
    [Authorize(Roles.Admin)]
    public async Task<IActionResult> DeleteBlogCategoryAsync(string name)
    {
        try
        {
            await _blogCategoryService.DeleteBlogCategoryAsync(name);

            return ApiServiceResponse.ApiServiceResult(new ServiceResponse());
        }
        catch (Exception ex)
        {
            return ApiServiceResponse.ApiServiceResult(new ServiceResponse(ex));
        }
    }
}