using System.ComponentModel.DataAnnotations;

namespace BloggingClient.Models;

public class CompanyModel
{
    [Required]
    public string Name { get; set; }
    //public string Route { get; set; }
}