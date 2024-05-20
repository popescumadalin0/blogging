using System.Threading.Tasks;
using DataBaseLayout.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayout.DbContext;

public class Context : IdentityDbContext<User, Role, string>, IContext
{
    public DbSet<Blog> Blogs { get; set; }

    public DbSet<BlogCategory> BlogCategories { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public Context(DbContextOptions<Context> options)
        : base(options) { }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(name: "Users");
        });
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable(name: "Roles");
        });
        modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable(name: "UserClaims");
        });
        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable(name: "UserRoles");
        });
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable(name: "RoleClaims");
        });
        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable(name: "UserTokens");
        });
        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable(name: "UserLogins");
        });

        modelBuilder.Entity<Blog>().HasOne(x => x.User)
            .WithMany(x => x.Blogs)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<Blog>().HasOne(x => x.BlogCategory)
            .WithMany(x => x.Blogs)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(x => x.BlogCategoryName);

        modelBuilder.Entity<Comment>().HasOne(x => x.Blog)
            .WithMany(x => x.Comments)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(x => x.BlogId);

        modelBuilder.Entity<Comment>().HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .OnDelete(DeleteBehavior.NoAction)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<Blog>().Navigation(t => t.Comments).AutoInclude();

        modelBuilder.Entity<User>().Navigation(t => t.Blogs).AutoInclude();

        modelBuilder.Entity<BlogCategory>().Navigation(t => t.Blogs).AutoInclude();
    }
}