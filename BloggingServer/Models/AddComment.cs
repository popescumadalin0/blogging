using System;

namespace Models;

public class AddComment
{
    public string Description { get; set; }

    public string UserId { get; set; }

    public Guid BlogId { get; set; }
}