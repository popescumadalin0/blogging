using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloggingClient.Models;

public class AddTicketModel
{
    [Required]
    public int Price { get; set; }

    [Required]
    public string Currency { get; set; }

    public string Image { get; set; }

    public List<AddLayoverModel> Layovers { get; set; } = new List<AddLayoverModel>();
}