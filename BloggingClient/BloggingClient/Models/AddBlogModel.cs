using System.ComponentModel.DataAnnotations;

namespace BloggingClient.Models;

public class AddBlogModel
{
    [Required(ErrorMessage = "You must set a title!")]
    public string Title { get; set; }

    [Required(ErrorMessage = "You must set a category!")]
    public string BlogCategoryName { get; set; }

    public byte[] Image { get; set; }

    public string Description { get; set; }
}