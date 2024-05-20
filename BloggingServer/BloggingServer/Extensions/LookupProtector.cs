using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BloggingServer.Extensions;

public class LookupProtector : ILookupProtector
{
    public string Protect(string keyId, string data)
    {
        return new string(data?.Reverse().ToArray());
    }

    public string Unprotect(string keyId, string data)
    {
        return new string(data?.Reverse().ToArray());
    }
}