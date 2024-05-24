using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Refit;
using SDK.RefitModels;

namespace SDK.Interfaces;

/// <summary>
/// Here we add the endpoints for the entire application
/// </summary>
public interface IBloggingApiClient
{
    Task<ApiResponseMessage<List<User>>> GetUsersAsync();

    Task<ApiResponseMessage<User>> GetUserAsync(string id);

    Task<ApiResponseMessage> DeleteUserAsync(string id);

    Task<ApiResponseMessage> RegisterUserAsync(AddUser user);

    Task<ApiResponseMessage<LoginResponse>> LoginUserAsync(LoginRequest request);

    Task<ApiResponseMessage> UpdateUserAsync(UpdateUser user);

    Task<ApiResponseMessage<List<BlogCategory>>> GetBlogCategoriesAsync();

    Task<ApiResponseMessage> CreateBlogCategoryAsync(BlogCategory blogCategory);

    Task<ApiResponseMessage> DeleteBlogCategoryAsync(string name);

    Task<ApiResponseMessage<List<Blog>>> GetBlogsAsync();

    Task<ApiResponseMessage<Blog>> GetBlogAsync(string id);

    Task<ApiResponseMessage> CreateBlogAsync(AddBlog blog);

    Task<ApiResponseMessage> DeleteBlogAsync(string id);

    Task<ApiResponseMessage<List<Comment>>> GetCommentsAsync();

    Task<ApiResponseMessage> CreateCommentAsync(Comment comment);

    Task<ApiResponseMessage> DeleteCommentAsync(string id);
}