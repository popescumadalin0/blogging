using System;

namespace BloggingClient.Models;

public class FlightFilter
{
    public LocationModel Leaving { get; set; }
    public LocationModel Going { get; set; }
    public DateTime Departing { get; set; }
    public DateTime Arriving { get; set; }
    public int NoAdults { get; set; }
    public int NoChildren { get; set; }
}