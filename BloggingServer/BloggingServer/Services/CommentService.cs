using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingServer.Repositories.Interfaces;
using BloggingServer.Services.Interfaces;
using Models;

namespace BloggingServer.Services;

public class CommentService : ICommentService
{
    private readonly IRepositoryBase<DataBaseLayout.Models.Comment> _commentRepository;

    public CommentService(IRepositoryBase<DataBaseLayout.Models.Comment> commentRepository)
    {
        _commentRepository = commentRepository;
    }

    /// <inheritdoc />
    public async Task<List<Comment>> GetCommentsAsync()
    {
        var entities = await _commentRepository.GetAllAsync();

        return entities.Select(
                c => new Comment()
                {
                    UserId = c.UserId,
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
        var entity = new DataBaseLayout.Models.Comment()
        {
            UserId = comment.UserId,
            CreatedDate = DateTime.UtcNow,
            Description = comment.Description,
            Id = Guid.NewGuid(),
            BlogId = comment.BlogId,
        };

        await Task.Run(() => _commentRepository.Add(entity));
    }

    /// <inheritdoc />
    public async Task DeleteCommentAsync(Guid id)
    {
        var entity = await _commentRepository.GetAsync(x => x.Id == id);

        _commentRepository.Remove(entity);
    }

}