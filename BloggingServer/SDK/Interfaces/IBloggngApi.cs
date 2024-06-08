using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Refit;

namespace SDK.Interfaces;

/// <summary>
/// Here we add the endpoints for the entire application
/// </summary>
public interface IBloggingApi
{
    [Get("/api/user")]
    [Headers("Authorization: Bearer")]
    Task<List<User>> GetUsersAsync();

    [Get("/api/user/{id}")]
    [Headers("Authorization: Bearer")]
    Task<User> GetUserAsync(string id);

    [Get("/api/user/username/{username}")]
    [Headers("Authorization: Bearer")]
    Task<User> GetUserByUsernameAsync(string username);

    [Delete("/api/user/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteUserAsync(string id);

    [Post("/api/user/register")]
    Task RegisterUserAsync(AddUser user);

    [Post("/api/user/login")]
    Task<LoginResponse> LoginUserAsync(BlogLoginRequest request);

    [Put("/api/user")]
    [Headers("Authorization: Bearer")]
    Task UpdateUserAsync(UpdateUser user);

    [Get("/api/blogCategory")]
    [Headers("Authorization: Bearer")]
    Task<List<BlogCategory>> GetBlogCategoriesAsync();

    [Get("/api/blogCategory/used")]
    [Headers("Authorization: Bearer")]
    Task<List<BlogCategory>> GetUsedBlogCategoriesAsync();

    [Post("/api/blogCategory")]
    [Headers("Authorization: Bearer")]
    Task CreateBlogCategoryAsync(BlogCategory blogCategory);

    [Delete("/api/blogCategory/{name}")]
    [Headers("Authorization: Bearer")]
    Task DeleteBlogCategoryAsync(string name);

    [Post("/api/blog/filter")]
    [Headers("Authorization: Bearer")]
    Task<List<Blog>> GetBlogsAsync(BlogFilter filter = null);

    [Get("/api/blog/username/{username}")]
    [Headers("Authorization: Bearer")]
    Task<List<Blog>> GetBlogsByUserAsync(string username);

    [Get("/api/blog/{id}")]
    [Headers("Authorization: Bearer")]
    Task<Blog> GetBlogAsync(string id);

    [Post("/api/blog")]
    [Headers("Authorization: Bearer")]
    Task CreateBlogAsync(AddBlog blog);

    [Delete("/api/blog/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteBlogAsync(string id);

    [Get("/api/comment")]
    [Headers("Authorization: Bearer")]
    Task<List<Comment>> GetCommentsAsync();

    [Get("/api/comment/blog/{id}")]
    [Headers("Authorization: Bearer")]
    Task<List<Comment>> GetCommentsByBlogAsync(string id);

    [Post("/api/comment")]
    [Headers("Authorization: Bearer")]
    Task CreateCommentAsync(AddComment comment);

    [Delete("/api/comment/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteCommentAsync(string id);
}

