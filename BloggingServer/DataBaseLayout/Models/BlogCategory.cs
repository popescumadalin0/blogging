using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayout.Models;

[PrimaryKey(nameof(Name))]
public class BlogCategory
{
    public string Name { get; set; }

    public ICollection<Blog> Blogs { get; set; }
}