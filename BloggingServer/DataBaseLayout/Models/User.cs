using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DataBaseLayout.Models;

public class User : IdentityUser
{
    [ProtectedPersonalData]
    public string CNP { get; set; }

    public byte[] ProfileImage { get; set; }

    public byte[] Document { get; set; }

    [PersonalData]
    public string FirstName { get; set; }

    [PersonalData]
    public string LastName { get; set; }

    public ICollection<Booking> Bookings { get; set; }

}