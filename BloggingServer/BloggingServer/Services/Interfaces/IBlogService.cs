using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using Models;

namespace BloggingServer.Services.Interfaces;

public interface IBlogService
{
    /// <summary/>
    Task<List<Blog>> GetBlogsAsync();

    /// <summary/>
    Task AddBlogAsync(AddBlog blog);

    /// <summary/>
    Task DeleteBlogAsync(Guid id);
}