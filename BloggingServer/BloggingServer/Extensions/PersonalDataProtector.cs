using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BloggingServer.Extensions;

public class PersonalDataProtector : IPersonalDataProtector
{
    public string Protect(string data)
    {
        return new string(data?.Reverse().ToArray());
    }

    public string Unprotect(string data)
    {
        return new string(data?.Reverse().ToArray());
    }
}