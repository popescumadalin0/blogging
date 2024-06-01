using System;

namespace Models;

public class User
{
    public string Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public DateTime JoinedDate { get; set; }

    public string ProfileImage { get; set; }

    public int NumberOfBlogs { get; set; }
}