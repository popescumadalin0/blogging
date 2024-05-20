using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayout.Models;

[PrimaryKey(nameof(Id))]
public class Comment
{
    public Guid Id { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    public string UserId { get; set; }

    [Required]
    public User User { get; set; }

    public string BlogId { get; set; }

    [Required]
    public Blog Blog { get; set; }
}