using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloggingClient.Models;

public class AddLayoverModel
{
    public Guid Id { get; set; }

    [Required]
    public string StartPointCountry { get; set; }

    [Required]
    public string StartPointAirport { get; set; }

    [Required]
    public string DestinationAirport { get; set; }

    [Required]
    public DateTime DepartureTime { get; set; }

    [Required]
    public DateTime ArrivalDuration { get; set; }

    [Required]
    public string DestinationCity { get; set; }

    [Required]
    public string DestinationCountry { get; set; }

    [Required]
    public string StartPointCity { get; set; }

    [Required]
    public int Order { get; set; }

    public string CompanyName { get; set; }

    [Required]
    public int PlaneSeats { get; set; }

    public List<AddPlaneFacilityModel> PlaneFacilities { get; set; } = new List<AddPlaneFacilityModel>();
}