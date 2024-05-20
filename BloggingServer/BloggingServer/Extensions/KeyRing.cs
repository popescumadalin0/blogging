using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace BloggingServer.Extensions;

public class KeyRing : ILookupProtectorKeyRing
{
    public string this[string keyId] => "key";

    public string CurrentKeyId => "key";

    public IEnumerable<string> GetAllKeyIds()
    {
        return new string[] { "key" };
    }
}