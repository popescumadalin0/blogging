using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BloggingClient.Pages.Blog;

public partial class BlogCard : ComponentBase
{
    [Parameter]
    public global::Models.Blog Item { get; set; }

    [Parameter]
    public EventCallback<Guid> ItemClicked { get; set; }

    private async Task CellClick()
    {
        if (ItemClicked.HasDelegate)
        {
            await ItemClicked.InvokeAsync(Item.Id);
        }
    }
}