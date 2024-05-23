using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace BloggingServer.Services.Interfaces;

public interface IBlogCategoryService
{
    /// <summary/>
    Task<List<BlogCategory>> GetBlogCategoriesAsync();

    /// <summary/>
    Task CreateBlogCategoryAsync(BlogCategory model);

    /// <summary/>
    Task DeleteBlogCategoryAsync(string name);
}