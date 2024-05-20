using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayout.Models;

[PrimaryKey(nameof(Id))]
public class PlaneFacility
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Layover> Layovers { get; set; }
}