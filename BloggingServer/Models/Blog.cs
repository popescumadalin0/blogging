using System;

namespace Models;

public class Blog
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string BlogCategory { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public string UserId { get; set; }

    public string UserName { get; set; }
}