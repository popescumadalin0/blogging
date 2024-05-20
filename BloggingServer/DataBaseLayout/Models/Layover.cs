using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;


namespace DataBaseLayout.Models;
[PrimaryKey(nameof(Id))]
public class Layover
{
    public Guid Id { get; set; }

    public string StartPointCountry { get; set; }

    public string StartPointAirport { get; set; }

    public string DestinationAirport { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalDuration { get; set; }

    public string DestinationCity { get; set; }

    public string DestinationCountry { get; set; }

    public string StartPointCity { get; set; }

    public int Order { get; set; }

    public Guid TicketId { get; set; }

    [Required]
    public Ticket Ticket { get; set; }

    public Guid CompanyId { get; set; }

    [Required]
    public Company Company { get; set; }

    public ICollection<PlaneSeat> PlaneSeats { get; set; }

    public virtual ICollection<PlaneFacility> PlaneFacilities { get; set; }
}