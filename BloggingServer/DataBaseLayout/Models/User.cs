using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataBaseLayout.Models;

public class User : IdentityUser
{
    public byte[] ProfileImage { get; set; }

    public ICollection<Comment> Comments { get; set; }

    public ICollection<Blog> Blogs { get; set; }
}