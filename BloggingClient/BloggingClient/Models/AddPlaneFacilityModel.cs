using System;
using System.ComponentModel.DataAnnotations;

namespace BloggingClient.Models;

public class AddPlaneFacilityModel
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }
}