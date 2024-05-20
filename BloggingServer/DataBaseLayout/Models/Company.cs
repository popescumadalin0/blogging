using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayout.Models;

[PrimaryKey(nameof(Id))]
public class Company
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int Raiting { get; set; }

    public ICollection<Layover> Layovers { get; set; }
}