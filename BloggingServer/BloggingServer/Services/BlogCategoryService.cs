using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;
using Models;

namespace BloggingServer.Services;

public class BlogCategoryService : IBlogCategoryService
{
    private readonly IRepositoryBase<DataBaseLayout.Models.BlogCategory> _blogCategoryRepository;

    public BlogCategoryService(IRepositoryBase<DataBaseLayout.Models.BlogCategory> blogCategoryRepository)
    {
        _blogCategoryRepository = blogCategoryRepository;
    }

    /// <inheritdoc />
    public async Task<List<BlogCategory>> GetBlogCategoriesAsync()
    {
        var entities = await _blogCategoryRepository.GetAllAsync();

        return entities.Select(
                e => new BlogCategory()
                {
                    Name = e.Name,
                })
            .ToList();
    }

    public async Task<List<BlogCategory>> GetUsedBlogCategoriesAsync()
    {
        var entities = await _blogCategoryRepository.GetListAsync(bc => bc.Blogs.Any());

        return entities.Select(
                e => new BlogCategory()
                {
                    Name = e.Name,
                })
            .ToList();
    }

    /// <inheritdoc />
    public async Task CreateBlogCategoryAsync(BlogCategory model)
    {
        var entity = new DataBaseLayout.Models.BlogCategory()
        {
            Name = model.Name,
        };

        await Task.Run(() => _blogCategoryRepository.Add(entity));
    }

    /// <inheritdoc />
    public async Task DeleteBlogCategoryAsync(string name)
    {
        var entity = await _blogCategoryRepository.GetAsync(x => x.Name == name);

        _blogCategoryRepository.Remove(entity);
    }

}