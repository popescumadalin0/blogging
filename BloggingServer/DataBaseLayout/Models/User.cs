using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataBaseLayout.Models;

public class User : IdentityUser
{
    public byte[] ProfileImage { get; set; }

    public bool AcceptTerms { get; set; }

    public DateTime JoinedDate { get; set; }

    public ICollection<Comment> Comments { get; set; }

    public ICollection<Blog> Blogs { get; set; }
}