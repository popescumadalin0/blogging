using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Models;
using SDK.Interfaces;
using SDK.RefitModels;

namespace SDK.Clients;

/// <summary>
/// Here we add the endpoints for the entire application
/// </summary>
public class BloggingApiClient : RefitApiClient<IBloggingApi>, IBloggingApiClient
{
    private readonly IBloggingApi _apiClient;

    private readonly ILogger<BloggingApiClient> _logger;

    public BloggingApiClient(IBloggingApi apiClient, ILogger<BloggingApiClient> logger) : base()
    {
        _apiClient = apiClient;
        _logger = logger;
    }

    public async Task<ApiResponseMessage<List<User>>> GetUsersAsync()
    {
        try
        {
            var task = _apiClient.GetUsersAsync();
            var result = await Execute(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(GetUsersAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage<User>> GetUserAsync(string id)
    {
        try
        {
            var task = _apiClient.GetUserAsync(id);
            var result = await Execute(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(GetUserAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage<User>> GetUserByUsernameAsync(string username)
    {
        try
        {
            var task = _apiClient.GetUserByUsernameAsync(username);
            var result = await Execute(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(GetUserByUsernameAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage> DeleteUserAsync(string id)
    {
        try
        {
            var task = _apiClient.DeleteUserAsync(id);
            var result = await ExecuteWithNoContentResponse(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(DeleteUserAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage> RegisterUserAsync(AddUser user)
    {
        try
        {
            var task = _apiClient.RegisterUserAsync(user);
            var result = await ExecuteWithNoContentResponse(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(RegisterUserAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage<LoginResponse>> LoginUserAsync(BlogLoginRequest request)
    {
        try
        {
            var task = _apiClient.LoginUserAsync(request);
            var result = await Execute(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(LoginUserAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage> UpdateUserAsync(UpdateUser user)
    {
        try
        {
            var task = _apiClient.UpdateUserAsync(user);
            var result = await ExecuteWithNoContentResponse(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(UpdateUserAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage<List<BlogCategory>>> GetBlogCategoriesAsync()
    {
        try
        {
            var task = _apiClient.GetBlogCategoriesAsync();
            var result = await Execute(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(GetBlogCategoriesAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage> CreateBlogCategoryAsync(BlogCategory blogCategory)
    {
        try
        {
            var task = _apiClient.CreateBlogCategoryAsync(blogCategory);
            var result = await ExecuteWithNoContentResponse(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(CreateBlogCategoryAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage> DeleteBlogCategoryAsync(string name)
    {
        try
        {
            var task = _apiClient.DeleteBlogCategoryAsync(name);
            var result = await ExecuteWithNoContentResponse(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(DeleteBlogCategoryAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage<List<Blog>>> GetBlogsAsync()
    {
        try
        {
            var task = _apiClient.GetBlogsAsync();
            var result = await Execute(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(GetBlogsAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage<List<Blog>>> GetBlogsByUserAsync(string username)
    {
        try
        {
            var task = _apiClient.GetBlogsByUserAsync(username);
            var result = await Execute(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(GetBlogsByUserAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage<Blog>> GetBlogAsync(string id)
    {
        try
        {
            var task = _apiClient.GetBlogAsync(id);
            var result = await Execute(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(GetBlogAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage> CreateBlogAsync(AddBlog blog)
    {
        try
        {
            var task = _apiClient.CreateBlogAsync(blog);
            var result = await ExecuteWithNoContentResponse(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(CreateBlogAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage> DeleteBlogAsync(string id)
    {
        try
        {
            var task = _apiClient.DeleteBlogAsync(id);
            var result = await ExecuteWithNoContentResponse(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(DeleteBlogAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage<List<Comment>>> GetCommentsAsync()
    {
        try
        {
            var task = _apiClient.GetCommentsAsync();
            var result = await Execute(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(GetCommentsAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage<List<Comment>>> GetCommentsByBlogAsync(string id)
    {
        try
        {
            var task = _apiClient.GetCommentsByBlogAsync(id);
            var result = await Execute(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(GetCommentsByBlogAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage> CreateCommentAsync(AddComment comment)
    {
        try
        {
            var task = _apiClient.CreateCommentAsync(comment);
            var result = await ExecuteWithNoContentResponse(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(CreateCommentAsync)}");
            throw;
        }
    }

    public async Task<ApiResponseMessage> DeleteCommentAsync(string id)
    {
        try
        {
            var task = _apiClient.DeleteCommentAsync(id);
            var result = await ExecuteWithNoContentResponse(task);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error executing {nameof(DeleteCommentAsync)}");
            throw;
        }
    }
}
