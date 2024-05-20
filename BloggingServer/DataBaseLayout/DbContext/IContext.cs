using System.Threading.Tasks;
using DataBaseLayout.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayout.DbContext;

public interface IContext
{
    DbSet<Blog> Blogs { get; set; }

    DbSet<BlogCategory> BlogCategories { get; set; }

    DbSet<Comment> Comments { get; set; }

    Task<int> SaveChangesAsync();
}