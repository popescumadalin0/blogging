using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace BloggingServer.Services.Interfaces;

public interface ICommentService
{
    /// <summary/>
    Task<List<Comment>> GetCommentsAsync();

    /// <summary/>
    Task AddCommentAsync(AddComment comment);

    /// <summary/>
    Task DeleteCommentAsync(Guid id);
}