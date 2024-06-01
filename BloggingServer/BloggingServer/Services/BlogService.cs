using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;
using DataBaseLayout.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using User = DataBaseLayout.Models.User;

namespace BloggingServer.Services;

public class BlogService : IBlogService
{
    private readonly IRepositoryBase<DataBaseLayout.Models.Blog> _blogRepository;

    private readonly UserManager<User> _userManager;

    public BlogService(IRepositoryBase<DataBaseLayout.Models.Blog> blogRepository, UserManager<User> userManager)
    {
        _blogRepository = blogRepository;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<List<Models.Blog>> GetBlogsAsync()
    {
        var entities = await _blogRepository.GetAllAsync();

        return entities.Select(
                c => new Models.Blog()
                {
                    Description = c.Description,
                    Id = c.Id,
                    UserName = c.User.UserName,
                    BlogCategory = c.BlogCategoryName,
                    Image = Convert.ToBase64String(c.Image),
                    Title = c.Title,
                    CreatedDate = c.CreatedTime,
                })
            .OrderBy(c => c.CreatedDate)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<List<Models.Blog>> GetBlogsByUserAsync(string username)
    {
        var user = await _userManager.Users.FirstAsync(u => u.UserName == username);

        var entities = await _blogRepository.GetListAsync(b => b.UserId == user.Id);

        return entities.Select(
                c => new Models.Blog()
                {
                    Description = c.Description,
                    Id = c.Id,
                    UserName = c.User.UserName,
                    BlogCategory = c.BlogCategoryName,
                    Image = Convert.ToBase64String(c.Image),
                    Title = c.Title,
                    CreatedDate = c.CreatedTime
                })
            .OrderBy(c => c.CreatedDate)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<Models.Blog> GetBlogAsync(Guid id)
    {
        var entity = await _blogRepository.GetAsync(x => x.Id == id);

        return new Models.Blog()
        {
            Description = entity.Description,
            Id = entity.Id,
            UserName = entity.User.UserName,
            BlogCategory = entity.BlogCategoryName,
            Image = Convert.ToBase64String(entity.Image),
            Title = entity.Title,
            CreatedDate = entity.CreatedTime
        };
    }

    /// <inheritdoc />
    public async Task AddBlogAsync(AddBlog blog)
    {
        var user = await _userManager.Users.FirstAsync(u => u.UserName == blog.Username);
        var entity = new DataBaseLayout.Models.Blog()
        {
            UserId = user.Id,
            BlogCategoryName = blog.BlogCategoryName,
            Title = blog.Title,
            CreatedTime = DateTime.UtcNow,
            Image = Convert.FromBase64String(blog.Image),
            Description = blog.Description,
            Id = Guid.NewGuid(),
        };

        _blogRepository.Add(entity);
    }

    /// <inheritdoc />
    public async Task DeleteBlogAsync(Guid id)
    {
        var entity = await _blogRepository.GetAsync(x => x.Id == id);

        _blogRepository.Remove(entity);
    }

}