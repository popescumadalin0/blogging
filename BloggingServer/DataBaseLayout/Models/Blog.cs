using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataBaseLayout.Models;

[PrimaryKey(nameof(Id))]

public class Blog
{
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string BlogCategoryName { get; set; }

    [Required]
    public BlogCategory BlogCategory { get; set; }

    [Required]
    public byte[] Image { get; set; }

    public string Description { get; set; }

    public DateTime CreatedTime { get; set; }

    public string UserId { get; set; }

    [Required]
    public User User { get; set; }

    public ICollection<Comment> Comments { get; set; }

}