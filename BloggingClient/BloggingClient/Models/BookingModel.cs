using System;

namespace BloggingClient.Models;

public class BookingModel
{
    public string StartPointCountry { get; set; }
    public string StartPointCity { get; set; }
    public string StartPointAirport { get; set; }
    public string DestinationAirport { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string DestinationCountry { get; set; }
    public string DestinationCity { get; set; }
    public int Price { get; set; }
    public string Currency { get; set; }

}