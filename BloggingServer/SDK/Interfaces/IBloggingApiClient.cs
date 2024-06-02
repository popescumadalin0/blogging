using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using SDK.RefitModels;

namespace SDK.Interfaces;

/// <summary>
/// Here we add the endpoints for the entire application
/// </summary>
public interface IBloggingApiClient
{
    Task<ApiResponseMessage<List<User>>> GetUsersAsync();

    Task<ApiResponseMessage<User>> GetUserAsync(string id);

    Task<ApiResponseMessage<User>> GetUserByUsernameAsync(string username);

    Task<ApiResponseMessage> DeleteUserAsync(string id);

    Task<ApiResponseMessage> RegisterUserAsync(AddUser user);

    Task<ApiResponseMessage<LoginResponse>> LoginUserAsync(BlogLoginRequest request);

    Task<ApiResponseMessage> UpdateUserAsync(UpdateUser user);

    Task<ApiResponseMessage<List<BlogCategory>>> GetBlogCategoriesAsync();

    Task<ApiResponseMessage> CreateBlogCategoryAsync(BlogCategory blogCategory);

    Task<ApiResponseMessage> DeleteBlogCategoryAsync(string name);

    Task<ApiResponseMessage<List<Blog>>> GetBlogsAsync();

    Task<ApiResponseMessage<List<Blog>>> GetBlogsByUserAsync(string username);

    Task<ApiResponseMessage<Blog>> GetBlogAsync(string id);

    Task<ApiResponseMessage> CreateBlogAsync(AddBlog blog);

    Task<ApiResponseMessage> DeleteBlogAsync(string id);

    Task<ApiResponseMessage<List<Comment>>> GetCommentsAsync();

    Task<ApiResponseMessage<List<Comment>>> GetCommentsByBlogAsync(string id);

    Task<ApiResponseMessage> CreateCommentAsync(AddComment comment);

    Task<ApiResponseMessage> DeleteCommentAsync(string id);
}