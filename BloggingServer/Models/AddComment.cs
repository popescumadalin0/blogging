using System;

namespace Models;

public class AddComment
{
    public string Description { get; set; }

    public string Username { get; set; }

    public Guid BlogId { get; set; }
}