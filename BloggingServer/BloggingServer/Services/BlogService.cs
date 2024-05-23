using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;
using Models;

namespace BloggingServer.Services;

public class BlogService : IBlogService
{
    private readonly IRepositoryBase<DataBaseLayout.Models.Blog> _blogRepository;

    public BlogService(IRepositoryBase<DataBaseLayout.Models.Blog> blogRepository)
    {
        _blogRepository = blogRepository;
    }

    /// <inheritdoc />
    public async Task<List<Blog>> GetBlogsAsync()
    {
        var entities = await _blogRepository.GetAllAsync();

        return entities.Select(
                c => new Blog()
                {
                    UserId = c.UserId,
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
    public async Task AddBlogAsync(AddBlog blog)
    {
        var entity = new DataBaseLayout.Models.Blog()
        {
            UserId = blog.UserId,
            BlogCategoryName = blog.BlogCategoryName,
            Title = blog.Title,
            CreatedTime = DateTime.UtcNow,
            Image = Convert.FromBase64String(blog.Image),
            Description = blog.Description,
            Id = Guid.NewGuid(),
        };

        await Task.Run(() => _blogRepository.Add(entity));
    }

    /// <inheritdoc />
    public async Task DeleteBlogAsync(Guid id)
    {
        var entity = await _blogRepository.GetAsync(x => x.Id == id);

        _blogRepository.Remove(entity);
    }

}