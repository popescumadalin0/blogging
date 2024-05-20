using System.Threading.Tasks;
using DataBaseLayout.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBaseLayout.DbContext;

public interface IContext
{
    DbSet<Booking> Bookings { get; set; }
    DbSet<PlaneSeat> PlaneSeats { get; set; }
    DbSet<PlaneFacility> PlaneFacilities { get; set; }
    DbSet<Layover> Layovers { get; set; }
    DbSet<Company> Companies { get; set; }
    DbSet<Ticket> Tickets { get; set; }
    Task<int> SaveChangesAsync();
}