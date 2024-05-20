using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayout.Models;

[PrimaryKey(nameof(Id))]
public class Ticket
{
    public Guid Id { get; set; }

    public int Price { get; set; }

    public string Currency { get; set; }

    public byte[] Image { get; set; }

    public ICollection<Layover> Layovers { get; set; }
}