using System;
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

    [Delete("/api/user/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteUserAsync(string id);

    [Post("/api/user/register")]
    Task RegisterUserAsync(AddUser user);

    [Post("/api/user/login")]
    Task<LoginResponse> LoginUserAsync(LoginRequest request);

    [Put("/api/user")]
    [Headers("Authorization: Bearer")]
    Task UpdateUserAsync(UpdateUser user);

    [Get("/api/blogCategory")]
    [Headers("Authorization: Bearer")]
    Task<List<BlogCategory>> GetBlogCategoriesAsync();

    [Post("/api/blogCategory")]
    [Headers("Authorization: Bearer")]
    Task CreateBlogCategoryAsync(BlogCategory blogCategory);

    [Delete("/api/blogCategory/{name}")]
    [Headers("Authorization: Bearer")]
    Task DeleteBlogCategoryAsync(string name);

    [Get("/api/blog")]
    [Headers("Authorization: Bearer")]
    Task<List<Blog>> GetBlogsAsync();

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

    [Post("/api/comment")]
    [Headers("Authorization: Bearer")]
    Task CreateCommentAsync(Comment comment);

    [Delete("/api/comment/{id}")]
    [Headers("Authorization: Bearer")]
    Task DeleteCommentAsync(string id);
}

