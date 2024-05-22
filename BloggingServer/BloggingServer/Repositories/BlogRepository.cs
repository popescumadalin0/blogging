using BloggingServer.Repositories.Interfaces;
using DataBaseLayout.DbContext;
using DataBaseLayout.Models;

namespace BloggingServer.Repositories;

public class BlogRepository : RepositoryBase<Blog>
{
    public BlogRepository(IContext context) : base(context)
    {
    }
}