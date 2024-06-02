using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BloggingServer.Services;

public class CommentService : ICommentService
{
    private readonly IRepositoryBase<DataBaseLayout.Models.Comment> _commentRepository;

    private UserManager<DataBaseLayout.Models.User> _userManager;

    public CommentService(IRepositoryBase<DataBaseLayout.Models.Comment> commentRepository, UserManager<DataBaseLayout.Models.User> userManager)
    {
        _commentRepository = commentRepository;
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<List<Comment>> GetCommentsAsync()
    {
        var entities = await _commentRepository.GetAllAsync(source => source.Include(c => c.User));

        return entities.Select(
                c => new Comment()
                {
                    CreatedDate = c.CreatedDate,
                    Description = c.Description,
                    Id = c.Id,
                    UserImage = Convert.ToBase64String(c.User.ProfileImage),
                    UserName = c.User.UserName
                })
            .ToList();
    }

    public async Task<List<Comment>> GetCommentsByBlogAsync(Guid id)
    {
        var entities = await _commentRepository.GetListAsync(c => c.BlogId == id, source => source.Include(c => c.User));

        return entities.Select(
                c => new Comment()
                {
                    CreatedDate = c.CreatedDate,
                    Description = c.Description,
                    Id = c.Id,
                    UserImage = Convert.ToBase64String(c.User.ProfileImage),
                    UserName = c.User.UserName
                })
            .ToList();
    }

    /// <inheritdoc />
    public async Task AddCommentAsync(AddComment comment)
    {
        var user = await _userManager.Users.FirstAsync(u => u.UserName == comment.Username);
        var entity = new DataBaseLayout.Models.Comment()
        {
            UserId = user.Id,
            CreatedDate = DateTime.UtcNow,
            Description = comment.Description,
            Id = Guid.NewGuid(),
            BlogId = comment.BlogId,
        };

        _commentRepository.Add(entity);
    }

    /// <inheritdoc />
    public async Task DeleteCommentAsync(Guid id)
    {
        var entity = await _commentRepository.GetAsync(x => x.Id == id);

        _commentRepository.Remove(entity);
    }

}