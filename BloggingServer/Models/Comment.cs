using System;

namespace Models;

public class Comment
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public string UserName { get; set; }

    public string UserImage { get; set; }
}