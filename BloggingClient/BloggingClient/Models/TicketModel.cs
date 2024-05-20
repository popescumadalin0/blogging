using System;
using System.Collections.Generic;
using Models;

namespace BloggingClient.Models;

public class TicketModel
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

    public string CompanyName { get; set; }

    public int Price { get; set; }

    public string Currency { get; set; }

    public string Image { get; set; }

    public List<Layover> Layovers { get; set; }
}